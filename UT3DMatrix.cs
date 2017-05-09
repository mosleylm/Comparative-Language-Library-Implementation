using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Liam Mosley
CSE 565

UT3D Matrix, represents an UT 3D matrix using a minimal amount of space
implements enumerability

*/

namespace UpperTriangularMatrix {
	// This iterator iterates over the upper triangular matrix.
	// This is done in a row major fashion, starting with [0][0],
	// and includes all N*N elements of the matrix.
	public class UT3DMatrixEnumerator : IEnumerator {
		public UT3DMatrix matrixToEnum;
		public int row;
		public int col;
		public int dep;
		
		public UT3DMatrixEnumerator(UT3DMatrix matrix) {
			this.matrixToEnum = matrix;
			Reset();
		}
		public void Reset() {
			row = -1;
			col = -1;
			dep = -1;
		}
		public bool MoveNext() {
			dep++;

			if(!matrixToEnum.validInd(row, col, dep)) {
				col++;
				dep = 0;
				
				if(!matrixToEnum.validInd(row, col, dep)) {
					row++;
					col = 0;
					dep = 0;
				}
			}

			return matrixToEnum.validInd(row, col, dep);
		}
		object IEnumerator.Current {
			get {
				return Current;
			}
		}
		public int Current {
			get {
				try {
					return matrixToEnum.get(row, col, dep);
				}
				catch (IndexOutOfRangeException) {
					throw new InvalidOperationException();
				}
			}
		}
	}
	public class UT3DMatrix : IEnumerable {
		// Must use a one dimensional array, having minumum size.
		public int [] data;
		public int sizeN; 


		// Construct an NxN Upper Triangular Matrix, initialized to 0
		// Throws an error if N is non-sensical.
		public UT3DMatrix(int N) {
			sizeN = N;
			// find number of elements in UT3D
			int numNonZeros = skipLevel(N, N);
						
			data = new int[numNonZeros];
			for(int i=0; i<numNonZeros; i++) {
				data[i] = 0;
			}
		}
		// Returns the size of the matrix
		public int getSize() {
			return sizeN;
		}
		// Returns an upper triangular matrix that is the summation of a & b.
		// Throws an error if a and b are incompatible.
		public static UT3DMatrix operator + (UT3DMatrix a, UT3DMatrix b) {
			if(a.getSize() == b.getSize()) {
				UT3DMatrix newMatrix = new UT3DMatrix(a.getSize());
				int sz = a.getSize();;	
	
				for(int i=0; i<sz; i++) {
					for(int j=i; j<sz; j++) {
						for(int k=j; k<sz; k++) {
							newMatrix.set(i, j, k, a.get(i, j, k) + b.get(i, j, k));
							//Console.WriteLine(i + " " + j + " " + k);
						}
					}
				}

				return newMatrix;
			} else {
				throw new System.ArgumentException("Incompatible matrices");
			}	
		}

		// Set the value at index [r][c][d] to val.
		// Throws an error if [r][c][d] is an invalid index to alter.
		public void set(int r, int c, int d, int val) {
			if(validUT(r, c, d)) {
				
				data[accessFunc(r, c, d)] = val;
			} else {
				throw new System.ArgumentException("not in UT");
			}
		}
		// Returns the value at index [r][c][d]
		// Throws an error if [r][c][d] is an invalid index
		public int get(int r, int c, int d) {
			if(validInd(r, c, d)) {
				if(validUT(r, c, d)) {
					
					return data[accessFunc(r, c, d)];
				} else {
					return 0;
				}
			} else {
				throw new System.ArgumentException("invalid index, cant retreive");
			}
		}
		
		// Returns the position in the 1D array for [r][c].
		// Throws an error if [r][c] is an invalid index
		public int accessFunc(int r, int c, int d) {
			if(validUT(r, c, d)) {
				//return skipLevel(getSize(), r) + (getSize() - r)*(c - r) + (d - r);	
				return skipLevel(getSize(), r) + innerUTSkip(getSize() - r) - innerUTSkip((getSize() - r) - (c - r)) + d - c;
			} else {
				throw new System.ArgumentException("Not valid UT");
			}	
		}

		// Returns an enumerator for an upper triangular matrix
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		public UT3DMatrixEnumerator GetEnumerator() {
			return new UT3DMatrixEnumerator(this);
		}
	
		public bool validInd(int r, int c, int d) {
			if(r>=0 && c>=0 && d>=0 && r < getSize() && c < getSize() && d < getSize()) {
				return true;
			} else {
				return false;
			}
		}

		public bool validUT(int r, int c, int d) {
			if(validInd(r, c, d) && r <= c && c <= d) {
				return true;
			} else {
				return false;
			}
		}

		// Returns number of entries to skip for a given row
		// if size = row, returns # elements in UT
		public int skipLevel(int size, int row) {
			/*
			int numElements = size*(size+1)*(2*size+1)/6;
			int numElementsUnder = (size - row)*(size - row + 1)*(2*size-2*row+1)/6;	
			int lowerElementSkip = innerUTSkip(size, row);			
			*/
			//return numElements - numElementsUnder - lowerElementSkip;

			int runTot = 0;

			for(int i=0; i<row; i++) {
				runTot += innerUTSkip(size-i);
			}
	
			return runTot;
		}

		public int innerUTSkip(int size) {
			int NByN = size * size;
			int UT = (NByN + size) / 2;; 

			return UT;
		}

		public static void Main(String [] args) {
			const int N = 5;
			UT3DMatrix ut1 = new UT3DMatrix(N);
			UT3DMatrix ut2 = new UT3DMatrix(N);
			for (int r=0; r<N; r++) {
				ut1.set(r, r, r, 1);
				for (int c=r; c<N; c++) {
					for (int d=c; d<N; d++) {
						ut2.set(r, c, d, 1);
					}
				}
			}
			
			//Console.WriteLine("first & second matrix made");
			//Console.WriteLine("skip test " + ut1.skipLevel(N, 3));	
			UT3DMatrix ut3 = ut1 + ut2;
			UT3DMatrixEnumerator ie = ut3.GetEnumerator();
			
			while (ie.MoveNext()) {
				Console.Write(ie.Current + " ");
			}
			Console.WriteLine();
			foreach (int v in ut3) {
				Console.Write(v + " ");
			}
			
			Console.WriteLine();
		}
	}
}
