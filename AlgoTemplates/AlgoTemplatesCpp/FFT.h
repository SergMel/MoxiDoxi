#include <iostream>
#include <vector>
#include <complex>

namespace AlgoTemplatesCpp
{
std::vector<std::complex<double>> RunFFT(const std::vector<int> &arr);

std::vector<std::complex<double>> RunFFT(const std::vector<std::complex<double>> &arr);

std::vector<std::complex<double>> RunIFFT(const std::vector<std::complex<double>> &arr);

std::vector<std::complex<double>> Multiply(const std::vector<std::complex<double>> &a, const std::vector<std::complex<double>> &b);
} // namespace AlgoTemplatesCpp