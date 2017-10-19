#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int main()
{
    printf("         moj algorytm          biblioteka C\n");
    printf("\n");
    licz(0.5,0);
    biblioteka(0.5);
    printf("\n");
    licz(1.0,0);
    biblioteka(1.0);
    printf("\n");
    licz(-1.0,0);
    biblioteka(-1.0);
    printf("\n");
    licz(0.3,0);
    biblioteka(0.3);
	printf("\n");
	liczTYL(0.3,0);
	printf("\n");
	// gcc -o a atan.c -lm
	int k;
	double zmienna= -1.0;
	double krok = 0.01;

	for(k=0;k<200;k++)
	{	zmienna = zmienna + krok;
		licz(zmienna,0);
	}


// ENO DRUKUJE
}

void licz(double x,int n){

    double suma = x;
    double element = x;
    double temp;

    while (n < 1000)
    {	
		temp = ( (-(x*x))*(2*n+1) ) / (2*n+3);  
        element = element * temp;          
        suma =suma + element;               
        n++;

    }

    printf("atan(%.2f) =%.16f\n" , x,suma);
}

void liczTYL(double x,int n){
	double suma=0;
    double element = x;
    double temp;
	double tab[1000];
	int z;

	while (n < 1000)
	{
		temp = ( (-(x*x))*(2*n+1) ) / (2*n+3);  
        element = element * temp;
        tab[n] = element;
		n++;		

	}
	for(z = 999; z >= 0;z--)
	{
		suma = suma + tab[z];
	}
	suma = suma + x;
	
	printf("atanTYL(%.2f) =%.16f" , x,suma);
	
	
}

void biblioteka(double z){

    double y;
    y = atan(z);
    printf("            %.16f ", y);
}


