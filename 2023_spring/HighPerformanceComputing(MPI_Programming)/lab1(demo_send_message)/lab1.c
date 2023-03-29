#include "stdio.h"
#include "mpi.h"

int main(argc, argv)
int argc;
char **argv;
{
    int rank_from_prev, rank_from_next;

    MPI_Status status;
    MPI_Request request[2];
    int i, num, msg1, msg2[8192];
    int rank, size, tag, next, prev, index;
    const int tag1 = 201, tag2 = 202;

    /* Start up MPI */

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    next = (rank + 1) % size;
    prev = (rank + size - 1) % size;


    MPI_Send(&rank, 1, MPI_INT, next, tag1, MPI_COMM_WORLD);
    MPI_Send(&rank, 1, MPI_INT, prev, tag2, MPI_COMM_WORLD);

    MPI_Recv(&rank_from_prev, 1, MPI_INT, prev, tag1, MPI_COMM_WORLD, &status);
    MPI_Recv(&rank_from_next, 1, MPI_INT, next, tag2, MPI_COMM_WORLD, &status);

    printf("Rank %d;rank_from_prev: %d \n", rank, rank_from_prev);
    printf("Rank %d;rank_from_next: %d \n", rank, rank_from_next);


    MPI_Finalize();

    return 0;
}