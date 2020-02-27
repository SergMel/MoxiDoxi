import sys
import operator

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

N = int(sys.stdin.readline()) 
M =  int(sys.stdin.readline())

uf = UF(N)

for i in range(0, M):
    tmp = list(map(int, input().split()))
    uf.union(tmp[0], tmp[1])

K = int(sys.stdin.readline()) 
cars = []
for i in range(0, K):
    tmp = list(map(int, input().split()))
    cars.append((tmp[0], tmp[1]))

cars.sort(key=operator.itemgetter(0), reverse=True)

roots = uf.getRoots()

gsizes = [uf.getSize(r) for r in roots ]
gsizes.sort( reverse=True)

j = 0
fail = False
i = 0
while i < len(gsizes):
    sz = gsizes[i]
    if j>= len(cars):
        fail = True
        break
    carTpl = cars[j]
    j+=1
    if (sz > carTpl[0]):
        fail = True
        break
    i+=carTpl[1]
    
if fail:
    print(0)
else:
    print(1)