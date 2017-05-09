using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Liam Mosley
CSE 565

Upper Triangular Matrix class
represents and UT matrix with a minimal amount of space used
implements enumerability
*/

namespace UTMatrix {
	// This iterator iterates over the upper triangular matrix.
	// This is done in a row major fashion, starting with [0][0],
	// and includes all N*N elements of the matrix.
	public class UTMatrixEnumerator : IEnumerator {
		public UTMatrix matrixToEnum;	
		public int row;
		public int col;	

		public UTMatrixEnumerator(UTMatrix matrix) {
			this.matrixToEnum = matrix;
			Reset();
		}
		public void Reset() {
			row = -1;
			col = -1;
		}
		public bool MoveNext() {
			col++;

			if(!matrixToEnum.checkValid(row, col)) {
				row++;
				col = 0;	
			} 

			return matrixToEnum.checkValid(row, col);
		}
		object IEnumerator.Current {
			get {
				return Current;
			}
		}
		public int Current {
			get {
				try {
					return matrixToEnum.get(row, col);
				}
				catch (IndexOutOfRangeException) {
					throw new InvalidOperationException();
				}
			}
		}
	}
	public class UTMatrix : IEnumerable {
		// Must use a one dimensional array, having minumum size.
		public int [] data;
		public int sizeN;

		// Construct an NxN Upper Triangular Matrix, initialized to 0
		// Throws an error if N is non-sensical.
		public UTMatrix(int N) {
			if(N > 0) {
				// detirmine size of 1D array
				int sizeOf1D = ((Convert.ToInt32(Math.Pow(N, 2))) + N) / 2;
				
				this.data = new int[sizeOf1D];
				this.sizeN = N;
				
				for(int i=0; i<data.Length; i++) {
					data[i] = 0;
				}

			} else {
				Console.WriteLine("ERROR, matrix size should be a positive integer");
			}
		}
		// Returns the size of the matrix
		public int getSize() {
			return sizeN;
		}
		// Returns an upper triangular matrix that is the summation of a & b.
		// Throws an error if a and b ae incompatible.
		public static UTMatrix operator + (UTMatrix a, UTMatrix b) {
			if(a.getSize() == b.getSize()) {	
				UTMatrix sumMatrix = new UTMatrix(a.getSize());
 
				for(int i=0; i<a.getSize(); i++) {
					for(int j=i; j<a.getSize(); j++) {
						sumMatrix.set(i, j, a.get(i, j) + b.get(i, j));
					}
				}
	
				return sumMatrix;
			} else {
				throw new System.ArgumentException("matrix a and b aren't compatible");
			}
		}
		// Set the value at index [r][c] to val.
		// Throws an error if [r][c] is an invalid index to alter.
		public void set(int r, int c, int val) {
			if(checkValidUT(r, c)) {
				data[accessFunc(r, c)] = val;
			} else {
				throw new System.ArgumentException("Invalid r/c");
			}
		}
		// Returns the value at index [r][c]
		// Throws an error if [r][c] is an invalid index
		public int get(int r, int c) {
			if(checkValid(r, c)) {
				if(checkValidUT(r, c)) {
					return data[accessFunc(r, c)];
				} else {
					return 0;
				}
			} else {
				throw new System.ArgumentException("Invalid r/c");
			}			
		}
		// Returns the position in the 1D array for [r][c].
		// Throws an error if [r][c] is an invalid index
		public int accessFunc(int r, int c) {
			if(checkValid(r, c)) {
				return (((Convert.ToInt32(Math.Pow(sizeN, 2)) + sizeN) / 2) - (((Convert.ToInt32(Math.Pow(sizeN - r, 2)) + sizeN - r) / 2))) + c - r;
			} else {
				throw new System.ArgumentException("Invalid r/c");
			}
		}
		// Returns an enumerator for an upper triangular matrix
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		public UTMatrixEnumerator GetEnumerator() {
			return new UTMatrixEnumerator(this);
		}

		public bool checkValidUT(int r, int c) {
			if(r >= 0 && c >= 0 && c >= r && r < sizeN && c < sizeN) {
				return true;
			} else {
				return false;
			}
		}

		public bool checkValid(int r, int c) {
			if(r >= 0 && c >= 0 && r < sizeN && c < sizeN) {
				return true;
			} else {
				return false;
			}
		}

		public static void Main(String [] args) {
			const int N = 5;
			UTMatrix ut1 = new UTMatrix(N);
			UTMatrix ut2 = new UTMatrix(N);
			for (int r=0; r<N; r++) {
				ut1.set(r, r, 1);
				for (int c=r; c<N; c++) {
					ut2.set(r, c, 1);
				}
			}
	
			foreach(int w in ut1) {
				Console.Write(w + " ");
			}			
			Console.WriteLine();
			foreach(int w in ut2) {
				Console.Write(w + " ");
			}
			Console.WriteLine();
			UTMatrix ut3 = ut1 + ut2;
			UTMatrixEnumerator ie = ut3.GetEnumerator();
			while (ie.MoveNext()) {
				Console.Write(ie.Current + " ");
			}
			Console.WriteLine();
			foreach (int v in ut3) {
				Console.Write(v + " ");
			}
			Console.WriteLine();

			Console.WriteLine(ut3.get(4,4));
		}
	}
}
