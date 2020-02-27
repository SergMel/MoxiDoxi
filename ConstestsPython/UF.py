class UF:

    def __init__(self, n):
        self.n = n
        self.parents = list(range(0, self.n))
        self.sizes = [1] * self.n
        self.groups = set(range(0, self.n))

    def getRoot(self,i):
        cur = i
        while(cur != self.parents[cur]):
            tmp = cur
            cur = self.parents[cur]
            self.parents[tmp] = self.parents[cur]
        return cur

    def isConnected(self, i, j):
        return self.getRoot(i) == self.getRoot(j)
        
    def getSize(self, i):
        root = self.getRoot(i)
        return self.sizes[root]

    def union(self, i, j):

        i = self.getRoot(i)
        j = self.getRoot(j)

        if i == j:
            return
        sizei = self.getSize(i)
        sizej = self.getSize(j)
        if sizei > sizej:        
            self.parents[j] = i        
            self.sizes[i] = sizei + sizej
            self.groups.remove(j)
        else:
            self.parents[i] = j
            self.sizes[j] = sizei + sizej
            self.groups.remove(i)

    def getRoots(self):
        return list(self.groups)


def Tests():
    Test1()
    Test2()
    Test3()
    # Test4()
           
def Test1():
    uf = UF(2)
    print(uf.isConnected(0, 0))
    print(not uf.isConnected(0, 1))
    uf.union(0, 1)
    print(uf.isConnected(0, 1))
    
def Test2():
    uf = UF(3)
    print(not uf.isConnected(0, 1))
    print(not uf.isConnected(0, 2))
    print(not uf.isConnected(1, 2))

    uf.union(0, 1)
    print(uf.isConnected(0, 1))
    print(not uf.isConnected(0, 2))
    print(not uf.isConnected(1, 2))

    uf.union(0, 2)
    print(uf.isConnected(0, 1))
    print(uf.isConnected(0, 2))
    print(uf.isConnected(1, 2))

def Test3():
    uf = UF(2)
    print(uf.isConnected(0, 0))
    print(not uf.isConnected(0, 1))
    uf.union(0, 1)
    print(uf.isConnected(0, 1))

    uf.union(0, 1)
    print(uf.isConnected(0, 1))

def Test4():
    n = 10000
    uf = UF(n+1)
    for i in range(1, n):
        uf.union(i - 1, i)
        if uf.isConnected(0, n - 1):
            print(False)
    uf.union(n - 2, n - 1)
    print(uf.isConnected(0, n - 1))

Tests()