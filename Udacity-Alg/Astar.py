import numpy as np

#Suppose there is a procedure inA that decides a language A in O(1) time.  
#Use dynamic programming (not memoization) to create an 
#algorithm that decides A* in O(n^2) time, where n is the 
#length of the input string.

#This is run by "Submit"
def inAstar(x, inA):
    """ Returns true if x is in A^* and false otherwise.
    The input inA should be assumed to be a function 
    that takes a string as input and returns True or False."""
    if (len(x) == 0):
        return inA(x)
    
    sa = [True]
    for i in range(1, len(x) + 1):
        sa.append(False)
        for j in range(0, i):
            if sa[j] and inA(x[j : i]):
                sa[i] = True
                break
        # print(sa)
    return sa[-1]

#This is run by "Test Run"
def main():
    def inA(x):
        return x in ['0','10']

    assert(inAstar('0101000010', inA))

    assert(not inAstar('000110010', inA))

    def inA(x):
        return x in ['01','10']

    assert(not inAstar('010', inA))
    
if __name__ == '__main__':
    main()


