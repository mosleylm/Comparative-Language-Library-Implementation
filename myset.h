#ifndef _MYSET_
#define _MYSET_

#include <cstring>
#include <iostream>
#include <initializer_list>
#include <algorithm>
#pragma warning(disable: 4996)
/*
Liam Mosley
CSE 565

.h file for myset.cpp
dynamically handles memory for an array representing a set of integers
only unique ints can be added to an array
upon an item being removed, entire array is shifted down
when capacity is hit, array size increases by 50%
*/

const bool DUMP = true;

class myset {
public:
	static const int MIN_CAPACITY = 10;
	int setSize;
	int setCapacity;

	int *data;	// must be dynamically allocate 1D array
	
	myset() {
		data = new int [MIN_CAPACITY];
		setSize = 0;
		setCapacity = MIN_CAPACITY;	
	}

	~myset() {
		delete [] data;
	}

	myset(const myset &other) {
		data = new int [other.capacity()];
		setSize = other.size();
		setCapacity = other.capacity();


		for(int i=0; i<other.size(); i++) {
			data[i] = other.data[i];
		}
	}

	myset &operator = (const myset &other) {
		if(this != &other) {
			delete [] data;

			data = new int [other.capacity()];
			setSize = other.size();
			setCapacity = other.capacity();

			for(int i=0; i<other.size(); i++) {
				data[i] = other.data[i];
			}
		}
		return *this;
	}

	myset(myset &&other) {
	
		setCapacity = other.capacity();
		setSize = other.size();

		data = new int [other.capacity()];
		

		for(int i=0; i<other.size(); i++) {
			data[i] = other.data[i];
		}
	}

	myset &operator = (myset &&other) 	{
		if(this != &other) {
			delete [] data;

			data = other.data;
			setSize = other.size();
			setCapacity = other.capacity();

			other.setSize = 0;
			other.setCapacity = 0;
			other.data = nullptr;
		}
		return *this;
	}

	// returns the number of elements in the set.
	int size() const {
		return setSize;
	}

	// Returns the current size of the array.
	int capacity() const {
		return setCapacity;
	}

	// Removes a value to the set. Return true iff the element
	// was successfully removed.
	bool remove(int value) {
		if(isElement(value)) {
			// remove element here, set ind to null
			int *temp = new int [capacity()];

			int tempInd = 0;
			
			for(int i=0; i<size(); i++) {
				if(data[i] != value) {
					temp[tempInd] = data[i];
					tempInd++;
				}
			}		

			int *hold = data;			
			data = temp;		
			setSize--;
			delete [] hold;

			return true;
		} else {
			return false; //if element isnt in data
		}
	}

	// Adds a value to the set. Return true iff the element
	// was successfully added.
	bool insert(int value) {
		if(isElement(value)) {
			return false;
		} else {
			if(capacity() == size()) {
				// if size = capacity then inc by 50%
				// create a new array

				int newcap = capacity()*1.5;
				int *temp = new int [newcap];
			
				setCapacity = newcap;

				for(int i=0; i < size(); i++) {
					temp[i] = data[i];
				}

				int *hold = data;
				data = temp;
				delete [] hold;
			} 
			// add item to lowest null index
			int ind = size();
			data[ind] = value;			
			setSize++;		
			
			for(int i=0; i<size(); i++) {
				std::cout << data[i];
			}

			return true;
		}		
	}

	// Returns true if value is part of set.
	bool isElement(int value) const {
				
		for(int i=0; i<size(); i++) {
			if(data[i] == value) {
				return true;
			}
		}

		return false;
	}
	
	// Returns an array containing the elements in the set.
	// This array is dynamically allocated an must be deleted by
	// the caller.
	int *getValues(int &sz) const {
		sz = size();
		int *result = new int[sz];
		
		for(int i=0; i < size(); i++) {
			result[i] = data[i];
		}
	
		return result;
	}
};

#endif
