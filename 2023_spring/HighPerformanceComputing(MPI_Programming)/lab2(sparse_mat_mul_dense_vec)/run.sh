mpicc $1.c -o $1.out
mpiexec -n $2 $1.out $3