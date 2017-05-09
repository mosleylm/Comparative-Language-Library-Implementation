Implementation project using C# and C++.

Myset is my own personal implementation of a set in C++ handling dynamic memory allocation where memory is stored as an array that is expanded as more entries are added to the set. All memory is accounted for.

MySerializer serializes objects that include data types of strings, bools, floats, and ints. 

The two UTMatrix classes are "library" implementations to handle efficient memory allocation for 2D and 3D upper triangular matrices. In it's current state UT3D is not time efficient but is memory efficient. Both classes make use of an "access function" to determine where values are stored in a 1D array.
