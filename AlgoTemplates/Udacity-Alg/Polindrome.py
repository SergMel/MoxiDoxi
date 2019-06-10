#Consider an arbitrary string X = x_1...x_n.  
#A subsequence of X is a string of the form 
#x_i_1 x_i_2...x_i_k ,where 1 <= i_1< ... < i_k <= n.  
#A string is palindromic if it is equal to its 
#own reverse (i.e. the string is the same whether 
#read backwards or forwards).  

#a) Derive a recurrence for L(i, j), remembering to include the base case?
#b) Why is your recurrence correct?
#c) Write a dynamic programming algorithm based on the recurrence for L(i, j) in python.  Use backtracking to construct a longest palindromic subsequence.
#d) What is its running time?

def lps(str):
    n = len(str) 
  
    L = [[0 for x in range(n)] for x in range(n)] 
  
    for i in range(n): 
        L[i][i] = 1
  
    for cl in range(2, n+1): 
        for i in range(n-cl+1): 
            j = i+cl-1
            if str[i] == str[j] and cl == 2: 
                L[i][j] = 2
            elif str[i] == str[j]: 
                L[i][j] = L[i+1][j-1] + 2
            else: 
                L[i][j] = max(L[i][j-1], L[i+1][j]); 
    print(L[0][n-1])
    i = 0
    j = n - 1
    sl = ""
    sr= ""
    while i <= j:
        if i == j:
            sl+=str[i]
            i+=1
        elif L[i][j] == L[i+1][j]:
            i+=1
        elif L[i][j] == L[i][j-1]:
            j-=1
        else:
            sl += str[i]
            sr=  str[j] + sr
            i+=1
            j-=1
    print(sl)
    print(sr)
    return sl+sr

def main():
    assert(lps('axayyybaxca4baza') in ['aabacabaa','axayyyaxa', 'aabaxabaa'])
                                        
if __name__ == "__main__":
    main()



    
