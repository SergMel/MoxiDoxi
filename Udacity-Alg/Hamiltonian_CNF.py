import pycosat

def getIndex(hPosition, nodeIndex, n):
	return n*hPosition + nodeIndex
	
def getPosition(index, n):
	return (index // n, index % n)

def every_node_presented(lst, G):
	node_count = len(G)
	pos_count = node_count
	for node in range(node_count):
		predicate = []
		for pos in range(pos_count):
			predicate.append(1 +node + pos*node_count)
		lst.append(predicate)


def every_pos_occupied(lst, G):
	node_count = len(G)
	pos_count = node_count
	for pos in range(pos_count):
		predicate = []
		for node in range(node_count):
			predicate.append(1 +node + pos*node_count)
		lst.append(predicate)

def no_duplication_in_path(lst, G):
	node_count = len(G)
	pos_count = node_count
	for node in range(node_count):
		for pos1 in range(pos_count - 1):
			for pos2 in range(pos1+1, pos_count):
				i1 = 1 +node + pos1*node_count
				i2 = 1 +node + pos2*node_count
				lst.append([-i1, -i2])

def no_duplication_nodes(lst, G):
	node_count = len(G)
	pos_count = node_count
	for pos in range(pos_count):
		for node1 in range(node_count - 1):
			for node2 in range(node1+1, node_count):
				i1 = 1 +node1 + pos*node_count
				i2 = 1 +node2 + pos*node_count
				lst.append([-i1, -i2])
				
def only_grpaph_edges(lst, G):
	node_count = len(G)
	pos_count = node_count
	for node1 in range(node_count ):
		for node2 in range(node_count):
			if node1 != node2 and G[node1][node2] == 0:
				for pos in range(pos_count - 1):
					i1 = 1 +node1 + pos*node_count
					i2 = 1 +node2 + (pos+1)*node_count
					lst.append([-i1, -i2])
				

def reduce_it(G):
        """ Input: an adjacency matrix G of arbitrary size represented as a list of lists.
        Output: the clauses of the cnf formula output in pycosat format.  
        Each clause is be reprented as a list of nonzero integers.
        Positive numbers indicate positive literals, negatives negative literal.
        Thus, the clause (x_1 \vee \not x_5 \vee x_4) is represented 
        as [1,-5,4].  A list of such lists is returned."""

        node_cnt = len(G)
        ret = []
        every_node_presented(ret, G)
        no_duplication_in_path(ret, G)
        every_pos_occupied(ret, G)
        no_duplication_nodes(ret, G)
        
        only_grpaph_edges(ret, G)
        
        return ret
        ##End Your Code HERE...

def main():
    #A graph with a hamiltonian path
    G = [[0, 0, 0, 1, 1], 
    [0, 0, 0, 0, 1], 
    [0, 0, 0, 1, 0], 
    [1, 0, 1, 0, 1], 
    [1, 1, 0, 1, 0]]

    clauses = reduce_it(G)

    sol = pycosat.solve(clauses)

    assert(sol != 'UNSAT')

    #A graph without a hamiltonian path
    G = [[0, 1, 1, 1, 1], 
    [1, 0, 0, 0, 0], 
    [1, 0, 0, 0, 0], 
    [1, 0, 0, 0, 1], 
    [1, 0, 0, 1, 0]]

    clauses = reduce_it(G)

    sol = pycosat.solve(clauses)

    assert (sol == 'UNSAT')

if __name__ == "__main__":
    main()
