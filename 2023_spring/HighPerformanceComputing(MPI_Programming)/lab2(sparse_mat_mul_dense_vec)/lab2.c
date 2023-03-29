#include "mpi.h"
#include <stdio.h>
#include "mmio.h"
#include "mmio.c"
#include <stdlib.h>
#include <string.h>
#include <time.h>

void print_array(const char *name, const int* array, int count){
	printf("Array: %s; Count: %d\n", name, count);

	int i;
	for(i=0; i<count; ++i){
		printf("%d,", array[i]);
	}
	printf("\n");
}


// global rank variable
int my_rank;
int main(int argc,char *args[])

{
	int size, rank;

	MPI_Init(&argc, &args);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	//  mmio test
	MM_typecode matcode;
	int matn, matm, nzeros=0;
	int  pimatm=0;
	int i,n;
	int nel;
	
    // FILE *f = fopen("west0067.mtx", "r");
	FILE *f;
	if (rank==0)
	{
		
		printf("args[1]: %s\n", args[1]);
		f = fopen(args[1], "r");
		mm_read_banner(f, &matcode);
		printf("  point 1");
		mm_read_mtx_crd_size(f, &pimatm, &matn, &nzeros);
		nel = nzeros/size;
		printf("pimatm=%d, matn=%d, nzeros=%d, nel=%d\n", pimatm, matn, nzeros, nel);
		
		if(mm_is_symmetric(matcode)){
			printf("Symmetric Matrix");
		}
	};

	int sendn = (int) pimatm;
	MPI_Bcast(&sendn, 1, MPI_INT, 0, MPI_COMM_WORLD);
	n = (int) sendn;
	if (rank == 0) printf("size=%d\n", size);
	printf("myrank=%d, pimatm=%d\n", rank, sendn);


    // int nel;

	// matm = pimatm;

	// nel = nzeros/size;



	// MPI_Bcast(&nrrpp, 1, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Bcast(&nzeros, 1, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Bcast(&pimatm, 1, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Bcast(&matn, 1, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Bcast(&nel, 1, MPI_INT, 0, MPI_COMM_WORLD);

    double A[nzeros];
    int rowsA[nzeros], colsA[nzeros];

    double B[nel];
    int rowsB[nel], colsB[nel];

    for(i=0; i<nel;++i){
        rowsB[i] = -1;
        colsB[i] = -1;
    }
    double V[matn];

	int inrow, incol;
	int sendcount[size];
	int displacements[size];
	double inval;
	i=0;
	if (rank==0)
	{
		for(i=0; i<nzeros; ++i)
		{
            fscanf(f, "%d %d %lg\n", &inrow, &incol, &inval);
			rowsA[i]=inrow;
			colsA[i]=incol;
			A[i]=inval;
		};
        // srand(time(NULL));
		srand(12);
        int i;
        printf("Vector:\n");
        for(i=0; i<matn; ++i){
            V[i] = ( (double)rand() )/ RAND_MAX;
            printf("%f,", V[i]);
        }
        printf("\n");

		sendcount[0] = nzeros - (size - 1) * (nel+1);
		displacements[0] = 0;
		for(i=1; i<size; ++i){
			sendcount[i] = nel+1;
			displacements[i] = displacements[i-1] + sendcount[i-1];
		}

		print_array("sendcount", sendcount, size);
		print_array("displacements", displacements, size);
	};

	MPI_Barrier(MPI_COMM_WORLD);

    MPI_Bcast(V, matn, MPI_DOUBLE, 0, MPI_COMM_WORLD);

	MPI_Scatterv(A, sendcount, displacements, MPI_DOUBLE, B, nzeros, MPI_DOUBLE, 0, MPI_COMM_WORLD);
    MPI_Scatterv(rowsA, sendcount, displacements, MPI_INT, rowsB, nzeros, MPI_INT, 0, MPI_COMM_WORLD);
	MPI_Scatterv(colsA, sendcount, displacements, MPI_INT, colsB, nzeros, MPI_INT, 0, MPI_COMM_WORLD);

	// MPI_Scatter(A, nel, MPI_DOUBLE, B, nel, MPI_DOUBLE, 0, MPI_COMM_WORLD);
    // MPI_Scatter(rowsA, nel, MPI_INT, rowsB, nel, MPI_INT, 0, MPI_COMM_WORLD);
    // MPI_Scatter(colsA, nel, MPI_INT, colsB, nel, MPI_INT, 0, MPI_COMM_WORLD);

    MPI_Barrier(MPI_COMM_WORLD);

    double partialResult[pimatm];
    double result[pimatm];

    for(i=0; i<pimatm;++i){
        partialResult[i] = 0;
    }

    for(i=0; i<nel; ++i){
        if(rowsB[i] > 0){
			partialResult[rowsB[i]-1] += B[i] * V[colsB[i]-1];
			if(mm_is_symmetric(matcode)){
				if(rowsB[i] != colsB[i]){
					partialResult[colsB[i]-1] += B[i] * V[rowsB[i]-1];
				}
			}
        }
    }

    MPI_Reduce(partialResult,result,pimatm,MPI_DOUBLE,MPI_SUM,0,MPI_COMM_WORLD);

	
    if(rank==0){
		printf("Result Vector:\n");
        for(i=0; i<pimatm; ++i){
            printf("%f,", result[i]);
        }
        printf("\n");

		if(f)fclose(f);
    }

	printf("Process %d before finalizing\n", rank);
	MPI_Finalize();
	// fclose(f);
	return  0;
}