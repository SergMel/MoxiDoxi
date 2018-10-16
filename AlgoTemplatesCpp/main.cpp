#include <iostream>
#include "FFT.h"

using namespace std;


int main()
{
    vector<int> arr(5);
    arr[4] = 1;
    arr[3] = 2;
    arr[2] = 0;
    arr[1] = 3;
    arr[0] = -1;

    vector<complex<double>> res = AlgoTemplatesCpp::RunFFT(arr);
    
    cout<<res[0]<<endl;
    
    res = AlgoTemplatesCpp::RunIFFT(res);
    
    cout<<res[0]<<endl;
    cout<<res[1]<<endl;
    cout<<res[2]<<endl;
    cout<<res[3]<<endl;
    cout<<res[4]<<endl;
    cout<<res[5]<<endl;
}