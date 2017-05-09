#include <iostream>
#include "myset.h"

/*
Liam Mosley 
CSE 565
main function for myset.cpp
myset described in helper file

Test cases includes are not my own, they are the intellectual property of Dr. Zmuda ***
*/

myset F() {
	myset result;

	for(int i=0; i<10; i++) {
		result.insert(i);
	}
	return result;
}

int main(int argc, char *argv[]) {
	myset s1 = F();
/*
	for(int i=0; i<5; i++) {
		s1.insert(i);
	}
	s1 = F();
*/
/*
	for(int i=0; i<10; i++) {
		s1.insert(-i);
	}

	for(int i=0; i<100; i++) {
		s2.insert(i);
	}

	s1 = s2;

	for(int i=1000; i<1010; i++) {
		s2.insert(i);
	}

	for(int i=0; i<100; i++) {
		if(s1.isElement(i)) {
			std::cout << "hello" << std::endl;
		}
	}
*/
/*
	for(int i=0; i<10; i++) {
		s1.insert(i);
	}
	s1 = F();
*/	
/*
	int sz;
	int *ptr = s1.getValues(sz);

	for(int i=0; i<sz; i++) {
		ptr[i] += 100;
	}
	//if(sz !=s1.size()) cout << "get values sz" << endl;
	//assert(s1.isElement(0));
	delete [] ptr;
*/
}

