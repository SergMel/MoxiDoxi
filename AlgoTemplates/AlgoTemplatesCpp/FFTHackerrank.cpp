#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
#include <algorithm>
#include <complex>

using namespace std;


const double PI = 4 * atan(1);
int GetClosestPower(int val)
{
    return 1 << (int)ceil(log2(val));
}

void internalFFT(vector<complex<double>> &arr, int reverse)
{
    if (arr.size() == 1)
    {
        return;
    }

    vector<complex<double>> ev(arr.size() / 2), odd(arr.size() / 2), powers(arr.size());

    double factor = (reverse * (2 * PI)) / arr.size();
    for (int i = 0; i < arr.size() / 2; i++)
    {
        ev[i] = arr[2 * i];
        odd[i] = arr[2 * i + 1];
        powers[2 * i] = polar((double)1, factor * (2 * i));
        powers[2 * i + 1] = polar((double)1, factor * (2 * i + 1));
    }

    internalFFT(ev, reverse);
    internalFFT(odd, reverse);

    for (int i = 0; i < arr.size() / 2; i++)
    {
        arr[i] = ev[i] + powers[i] * odd[i];
        arr[i + arr.size() / 2] = ev[i] - powers[i] * odd[i];
    }
}

vector<complex<double>> RunFFT(const vector<int> &arr)
{
    int N = GetClosestPower(arr.size());
    vector<complex<double>> res(N);

    for (int i = 0; i < arr.size(); i++)
    {
        res[i] = arr[i];
    }
    internalFFT(res, 1);
    return res;
}

vector<complex<double>> RunFFT(const vector<complex<double>> &arr)
{
    int N = GetClosestPower(arr.size());
    vector<complex<double>> res(N);

    for (int i = 0; i < arr.size(); i++)
    {
        res[i] = arr[i];
    }
    internalFFT(res, 1);
    return res;
}

vector<complex<double>> RunIFFT(const vector<complex<double>> &arr)
{
    int N = GetClosestPower(arr.size());
    vector<complex<double>> res(N);

    for (int i = 0; i < arr.size(); i++)
    {
        res[i] = arr[i];
    }
    internalFFT(res, -1);
    return res;
}


    vector<complex<double>> Multiply(const vector<complex<double>>& a, const vector<complex<double>>& b)
    {     
        if(a.size() != b.size())  
            throw new exception();
        vector<complex<double>> res(a.size());
        for(int i =0; i< a.size(); i++)
        {
            res[i] = a[i] * b[i];
        }
        return res;

    }

int main() {
    /* Enter your code here. Read input from STDIN. Print output to STDOUT */  
    int n;
    cin>>n;
    vector<int> coef1(200002);
    vector<int> coef2(200002);
      int res = 0;
  
    for(int i =0;i < n; i++)
    {
        int val;
        cin>>val;
        if (res == 0 && coef1[val] > 0)
        {
            res = 1;
        }
        coef1[val] = 1;
        coef2[100001 - val] = 1;
    }
    vector<complex<double>> r1 = RunFFT(coef1);
    vector<complex<double>> r2 = RunFFT(coef2);
    vector<complex<double>> r3 = Multiply(r1, r2);
    vector<complex<double>> r4 = RunIFFT(r3);
   

    for(int i = 100002; i < 200003; i++)
    {
        if(r4[i].real() / r4.size() > 0.99999)
        {
            // cout << (i -200001);
            // cout << r4[i];
            res++;
        }
    }
    cout<<res;
    
    return 0;
}
