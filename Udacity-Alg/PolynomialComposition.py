#Your task is twofold:
#1. Implement the polycomp(A,B) procedure below so 
#   that it returns the composition of polynomials A and B.
#2. Explain why your algorithm is O(n^3 log n) or 
#   (better still) O(n^3)
 
#In the procedure polycomp(A,B), the polynomials are given 
#as numpy arrays with type int. In numpy as in most numerical 
#packages, polynomials are represented as arrays with the 
#coefficient on the largest power oming first.  Thus, an 
#array A on length n would represent the polynomial 
#
#A[0] * x**(n-1) + A[1] * x**(n-2) + ...+ A[n-2] * x + A[n-1].
#
#We will use the same convention here.
#
#
#Two possible strategies include:
#1. Adapt one of the standard strategies for evaluating 
#   polynomials (e.g. Horner's rule) to the task of composing them.
#   If n is the larger order of the polynomials, this will give 
#   you a O(n^3 \log n) algorithm
#
#2. Use the fft (scipy.fft, scipy.ifft) to achieve a 
#   O(n^3) algorithm.  Be careful about the order of 
#   your coefficients.
#
#
#Some potentially useful calls are:
#
############################################################
#C = np.zeros(n, dtype=int) #to create an array of n zeros
#(http://docs.scipy.org/doc/numpy/reference/generated/numpy.zeros.html)
#
###########################################################
#C = np.convolve(A,B) #to convolve the arrays A and B
#(http://docs.scipy.org/doc/numpy/reference/generated/numpy.convolve.html)
#
############################################################
#V = np.polyval(A,x) #Evaluates A at the point values in x.  
# Note that A[-1] is the constant term
#(http://docs.scipy.org/doc/numpy/reference/generated/numpy.polyval.html)
#
############################################################
#c = fft(C, npts) #where npts is the number of points that will be output.
#C = ifft(c, npts) #where npts is the number of points that will be output.
#Note that C[0] is the constant term.
#(http://docs.scipy.org/doc/numpy/reference/routines.fft.html)
#
############################################################
#C = C[::-1] # to reverse the array C
#
############################################################
#C = C.real.round().astype(int) #To round complex numbers to ints along real axis
#
############################################################

import numpy as np
from numpy.fft import fft, ifft

def polycomp(A,B):
    """Computes A(B(x))"""
    n1 = (len(A) - 1) * (len(B)-1) + 1
    n = int( 2**np.ceil(np.log2(n1)))
    print(n1)
    print(n)
    arr = fft(B[::-1], n)
    print(arr)
    arr = np.polyval(A,arr)
    print(arr)
    arr = ifft(arr, n)
    print(arr)
    return arr[:n1][::-1].real.round().astype(int)

    

def main():
    A = np.array([1, 1, 1], dtype=int)
    B = np.array([ 2, 3], dtype=int)
    ans = polycomp(A, B)
    print(ans)

    x = 2
    print("1")
    print(np.polyval(A,np.polyval(B,x)))
    print("2")
    print(np.polyval(ans,x))
    
    assert type(ans) is np.ndarray
    assert np.array_equal(ans, np.array([2, 12, 23, 15,  4], dtype=int))

if __name__ == '__main__':
    main()