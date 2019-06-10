import sys, traceback
import numpy as np

def buildArtificialVariables(A, c):
    artificial = np.array( [[1.0 if i==j else 0.0 for j in range(A.shape[0])] for i in range(A.shape[0])] )
    artArr = np.concatenate((A, artificial), axis = 1)
    cfp = np.zeros((c.shape[0], 1))
    csp = np.full((artificial.shape[1], 1), -1.0)
    newc = np.concatenate((cfp, csp), axis = 0)
    basis = {key:A.shape[1]+key for key in range(A.shape[0])}
    return (artArr, newc, basis)

def pivotMatrix(A, b, c, cell):    
    row = cell[0]
    col = cell[1]
    pivotValue = A[row][col]
    A[row][col] = 1.0
    b[row] = b[row] / pivotValue
    for j in range(A.shape[1]):
        if j != col:
            A[row][j] = A[row][j] / pivotValue
    for i in range(A.shape[0]):
        if i == row:
            continue
        pivotRowValue = A[i][col]
        A[i][col] = 0.0
        b[i] = b[i] - b[row] * pivotRowValue  
        for j in range(A.shape[1]):
            if j == col:
                continue
            A[i][j] = A[i][j] - A[row][j]*pivotRowValue

    cval = c[col][0]
    for j in range(A.shape[1]):
        c[j] = c[j] - cval*A[row][j]
    return - cval*b[row]

def pivot(A, b, c, cell):
   return pivotMatrix(A, b, c, cell)

def pivotBasis(A, b, c, basis):
    for key in basis:
        pivot(A, b, c, (key, basis[key]))

def findFirstPositive(c):
    eps = 1e-5
    for j in range(len(c)):
        if c[j] > eps :
            return j 
    return None

def findRow(A, b, col):
    minRow = -1
    minVal = -1
    for i in range(A.shape[0]):
        if b[i] >=0 and A[i][col] > 0 and (minRow == -1 or minVal > b[i] / A[i][col] ):
            minRow = i
            minVal = b[i] / A[i][col]
    return minRow if minRow > -1 else None

def runSimplex(A, b, c, basis):
    A = np.copy(A).astype(float)
    b = np.copy(b).astype(float)
    c = np.copy(c).astype(float)
    basis = {key:basis[key] for key in basis}
    col = findFirstPositive(c)
    while col is not None:
        row = findRow(A, b, col)
        if row is None:
            return (None, None, None, None)
        pivot(A, b, c, (row, col))
        basis[row] = col
        col = findFirstPositive(c)
    return (A, b, c, basis)

def check(A, b, basis, colsCount):
    return all( b[key] == 0  for key in basis if basis[key] >= colsCount)
    

def getBasisFromArtificial(artArr, artBasis, A, b,c):
    basis = {key:artBasis[key] for key in artBasis if artBasis[key] < A.shape[1]}
    cols = set(basis[key] for key in basis)

    for row in range(A.shape[0]):
        if row in basis:
            continue
        for col in range(A.shape[1]):
            if col in cols or artArr[row][col] == 0:
                continue
            basis[row] = col
            cols.add(col)
            pivot(artArr, b, c, (row, col))
            break
    return basis
        

# Given: an m x n matrix A, a vector b in R^m, and a vector c in R^n.
# Output: a vector x in R^n such that c^Tx is maximum subject to the constraints
# Ax = b and x >= 0, if such a vector exists. Otherwise output None.
def maximize(A,b,c): 
    try:
        # enter your code here!
        c = np.copy(c).astype(float)
        A = np.copy(A).astype(float)
        b = np.copy(b).astype(float)
        (artArr, artC ,basis) = buildArtificialVariables(A, c)
        for row in basis:
            pivot(artArr, b, artC, (row, basis[row]))
    
        (artArr, artB, artC ,artBasis) = runSimplex(artArr, b, artC, basis)
        if artBasis is None or not check(artArr, artB, artBasis, A.shape[1]):
            return None
    
        basis = getBasisFromArtificial(artArr, artBasis, A, artB, artC)
   
        A = artArr[0:A.shape[0], 0:A.shape[1]]
        b = artB
        for row in basis:
            pivot(A, b, c, (row, basis[row]))
        (A, b, c, basis) = runSimplex(A, b, c, basis)
        if basis is None: 
            return None

        x = np.zeros((c.shape[0], 1))
        for row in basis:
            x[basis[row]] =b[row]
        return x
    except IndexError:   
        exc_type, exc_value, exc_traceback = sys.exc_info()
        raise ValueError(exc_traceback) 


def main():
    
    eps = 1e-5
    
    # Test case 1: Maximize
    # 2x - y  subject to 
    #  x      = 1
    #      y  = 1
    #  x , y >= 0
    A = np.array([[1, 0],
                  [0, 1]])
    b = np.transpose(np.array([[1, 1]]))
    c = np.transpose(np.array([[2,-1]]))
    x = maximize(A,b,c)
    v_user = np.dot(np.transpose(c), x)
    
    assert np.allclose(np.dot(A,x), b)
    assert np.all(x >= np.zeros_like(x))
    assert np.all(abs(v_user - 1) <= eps)
    
    # Test case 2: Maximize
    # 0.5x + 3y + z + 4w  subject to
    #    x +  y + z +  w  = 40
    #   2x +  y - z -  w  = 10
    #      -  y     +  w  = 10
    #    x ,  y , z ,  w >=  0
    A = np.array([[1, 1, 1, 1],
                  [2, 1,-1,-1],
                  [0,-1, 0, 1]])
    b = np.transpose(np.array([[40  , 10, 10]]))
    c = np.transpose(np.array([[ 0.5,  3,  1, 4]]))
    x = maximize(A,b,c)
    v_user = np.dot(np.transpose(c), x)
    
    assert np.allclose(np.dot(A,x), b)
    assert np.all(x >= np.zeros_like(x))
    assert np.all(abs(v_user - 115) <= eps)
    
    print("All test cases passed!")


def testPivot():
    eps = 1e-5
    
    # 1
    A = np.array([[2.0,1.0,2.0],
                  [3.0,3.0,1.0]])
    b = np.transpose(np.array([[4.0,3.0]]))
    c = np.transpose(np.array([[-4.0,-1.0,-1.0]]))
    x = maximize(A,b,c)
    v_user = np.dot(np.transpose(c), x)
    
    assert np.allclose(np.dot(A,x), b)
    assert np.all(x >= np.zeros_like(x))
    assert np.all(abs(v_user + 2.2) <= eps)
    
    # 3
    A = np.array([[ 1.,  -1.]])
    b = np.transpose(np.array([[0.]]))
    c = np.transpose(np.array([[1.]]))
    x = maximize(A,b,c)
    v_user = np.dot(np.transpose(c), x)
    
    assert np.allclose(np.dot(A,x), b)
    assert np.all(x >= np.zeros_like(x))
    
    
main()
testPivot()