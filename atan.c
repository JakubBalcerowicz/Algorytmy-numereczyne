#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int counter_tyl;
int counter_przod;
double blad_przod = 0;
double blad_tyl = 0;
double blad_przod_taylor = 0;
double blad_tyl_taylor = 0;
double blad[10000];
double blad_taylor[10000];

int main()
{
  printf("Sumowanie poprzez wyliczanie nastepnika\n");
  printf("\n");
  int k;
	double zmienna= -1.0;
	double krok = 0.00001;
	for(k=0;k<10000;k++)
	{
      zmienna = zmienna + krok;
      liczTYL(zmienna,0);
		  licz(zmienna,0);

      blad[k]=fabs(blad_tyl);
      //printf("sredni blad alrogrytmu %.16f\n",blad[k]);
        if(fabs(blad_tyl) > fabs(blad_przod))
        {
          counter_przod += 1;
        }
        else
        {
          counter_tyl += 1;
        }
  }
  int total = 10000;
  printf("przedzial [-1 ,-0.9]sumowanie od tylu: %.2lf%%  sumowanie od przodu %.2lf%% \n ", (((double)counter_tyl)/total)*100,(((double)counter_przod)/total)*100);
  counter_tyl=0;
  counter_przod=0;
  int l;
  for(l=0;l<10000;l++)
  {
      zmienna = zmienna + krok;
      wzor(zmienna);
      wzorTYL(zmienna);
      blad_taylor[l]=fabs(blad_tyl_taylor);
      //printf("przod %.16f  tyl %.16f\n",blad_przod, blad_tyl);
      if(fabs(blad_tyl_taylor) > fabs(blad_przod_taylor))
      {
        counter_przod += 1;
      }
      else
      {
        counter_tyl += 1;
      }

  }
  printf("przedzial [-1 ,-0.9]sumowanie TAYLOR od tylu: %.2lf%%  sumowanie od TAYLOR przodu %.2lf%% \n ", (((double)counter_tyl)/total)*100,(((double)counter_przod)/total)*100);
  counter_tyl=0;
  int p;
  int z = 0;
  int zz = 0;
  int zzz = 0;
  for (p=0;p<10000;p++){

    if(blad[p]>blad_taylor[p])
      z = z +1;
    else if(blad[p]<blad_taylor[p])
      zz = zz +1;
    else
      zzz=zzz+1;

  }
  printf("wiekszy blad w natsepniku:%d wiekszy blad talor:%d rowny blad:%d",z,zz,zzz);


}

void licz(double x,int n)
{
    double suma = x;
    double element = x;
    double temp;
    double y;
    y = atan(x);
      while (n < 1000)
      {
		      temp = ( (-(x*x))*(2*n+1) ) / (2*n+3);
          element = element * temp;
          suma =suma + element;
          n++;
      }
   blad_przod = y - suma;
   //printf("licz przod nastepnik: %.16f\n",suma);
}

void liczTYL(double x,int n)
{
	double suma=0;
  double element = x;
  double temp;
	double tab[1000];
	int z;
  double y;
  y = atan(x);
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
  blad_tyl = y - suma;
}
void wzor(double x)
{
  double sum = 0.0;
  double y;
  y = atan(x);
  int i;
  for(i = 0; i < 1000; i++)
  {
    double top = pow(-1, i) * pow(x, 2 * i + 1);
    double bottom = (2 * i + 1);
    sum = sum + top / bottom;
  }
   blad_przod_taylor = y - sum;
  //printf("wzor taylora %.16f zwykly%.16f ",sum,atan(x));
}

void wzorTYL(double x)
{
  double sum = 0.0;
  double y;
  double element;
  double tab[1000];
  int z;
  y = atan(x);
  int i;
  for(i = 0; i < 1000; i++)
  {
    double top = pow(-1, i) * pow(x, 2 * i + 1);
    double bottom = (2 * i + 1);
    element = top / bottom;
    tab[i]= element;
  }
  for(z = 999; z >= 0;z--)
  {
      sum = sum + tab[z];
  }

   blad_tyl_taylor = y - sum;
  //printf("wzor taylora %.16f zwykly%.16f \n",sum,atan(x));
}
