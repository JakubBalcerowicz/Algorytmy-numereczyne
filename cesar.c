#include <stdio.h>
#include <stdlib.h>
#include <string.h>
int main()
{
  char label[11] = "TEKSTJAWNY";
  int i;
  int key = 1;
    for(i = 0; i < 10; i++){
      int temp;
      temp = (int)label[i]+key;
      printf("%c",temp);
    }
    printf("\n");

}
