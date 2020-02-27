import sys
import operator

N = int(sys.stdin.readline()) 

reactorsinit = list(map(int, input().split()))

r2 = []

K = int(sys.stdin.readline()) 

cables = dict()
for i in range(0, K):
    tmp = list(map(int, input().split()))
    
    frst = tmp[0] - 1
    if (frst not in cables):
        cables[frst] = []
    scnd = tmp[1] - 1
    if (scnd not in cables):
        cables[scnd] = []
    
    cables[frst].append((scnd, tmp[2]))
    cables[scnd].append((frst, tmp[2]))


reactors = [(i, reactorsinit[i]) for i in range(0, len(reactorsinit))]

for itm in reactors:
    
    if (itm[0] not in cables):
        r2.append((itm[0], itm[1], sys.maxsize) )
        continue
    lst = [e[1] for e in cables[itm[0]] if reactorsinit[e[0]] <= itm[1] ]

    if(len(lst) < 1):
        r2.append((itm[0], itm[1], sys.maxsize) )
    else:
        r2.append((itm[0], itm[1], min(lst)) )

reactors = r2
reactors.sort(key=operator.itemgetter(1,2))

if (reactors is None):
    reactors = []
visited = set()
result = 0

for reactor in reactors:
    visited.add(reactor[0])
    if (reactor[0] not in cables):
        result += reactor[1]
        continue

    minCbl = None
    for nghb in cables[reactor[0]]:
        if (nghb[0] not in visited):
            continue

        if(minCbl is None or minCbl > nghb[1]):
            minCbl = nghb[1]

    if (minCbl is None or minCbl > reactor[1]):
        result+= reactor[1]
    else:
        result+= minCbl   

print(result)