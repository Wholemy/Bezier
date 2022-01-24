namespace Wholemy {
	public class Bzier {
		public const int MaxBound = 3;
		public const int MaxDepth = 54;
		public const double InitRoot = 0.0;
		public const double InitSize = 1.0;
		#region #class# Path 
		public class Path {
			public int Count;
			public Line Base;
			public Line Last;
			#region #new# (Item) 
			public Path(Line Item) {
				Item.Owner = this;
				Base = Last = Item;
				Count++;
			}
			#endregion
			#region #property# Items 
			public Line[] Items {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					var I = Count;
					var A = new Line[I];
					var S = Last;
					while(--I >= 0) {
						A[I] = S;
						S = S.Prev;
					}
					return A;
				}
			}
			#endregion
		}
		#endregion
		#region #class# Line 
		public class Line {
			public int C;
			public Line Intersected;
			protected private Rect _Rect;
			public readonly double X;
			public readonly double Y;
			public readonly double MX;
			public readonly double MY;
			public readonly double EX;
			public readonly double EY;
			#region #new# (MX, MY, EX, EY, Inverted, Depth, Root, Size) 
			public Line(double MX, double MY, double EX, double EY, bool Inverted = false, int Depth = 0, double Root = InitRoot, double Size = InitSize) {
				this.Inverted = Inverted;
				this.Depth = Depth;
				this.Root = Root;
				this.Size = Size;
				this.MX = MX;
				this.MY = MY;
				this.EX = EX;
				this.EY = EY;
				if(Inverted) {
					this.X = EX;
					this.Y = EY;
				} else {
					this.X = MX;
					this.Y = MY;
				}
			}
			#endregion
			#region #method# Div(root, A, B) 
			public virtual void Div(double root, out Line A, out Line B) {
				var x00 = MX;
				var y00 = MY;
				var x11 = EX;
				var y11 = EY;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var S = this.Size * root;
				var ss = this.Size - S;
				if(this.Inverted) {
					A = new Line(x01, y01, x11, y11, this.Inverted, this.Depth + 1, this.Root, ss);
					B = new Line(x00, y00, x01, y01, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					A = new Line(x00, y00, x01, y01, this.Inverted, this.Depth + 1, this.Root, S);
					B = new Line(x01, y01, x11, y11, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #method# Intersect(I) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public bool InIntersect(Line I) {
				if(this.Rect.Intersect(I.Rect)) {
					this.C++;
					I.C++;
					return true;
				}
				return false;
			}
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public bool Intersect(Line I) {
				return this.Rect.Intersect(I.Rect);
			}
			public bool Intersect(double X, double Y) {
				return this.Rect.Intersect(X, Y);
			}
			#endregion
			#region #get# Rect 
			public virtual Rect Rect {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get { if(_Rect == null) _Rect = Rect.From(this); return _Rect; }
			}
			#endregion
			#region #invisible# #get# Pastle 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public virtual Line Pastle {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Line(MX, MY, EX, EY, Inverted);
			}
			#endregion
			#region #invisible# #get# Return 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public virtual Line Return {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Line(MX, MY, EX, EY, !Inverted);
			}
			#endregion
			#region #invisible# #get# Invert 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public virtual Line Invert {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Line(EX, EY, MX, MY, !Inverted);
				}
			}
			#endregion
			#region #invisible# #get# Incest 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public virtual Line Incest {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Line(EX, EY, MX, MY, Inverted);
				}
			}
			#endregion
			public Path Owner;
			public Line Prev;
			public Line Next;
			public double InRoot {
				get {
					if(Inverted) return 1.0 - Root;
					return Root;
				}
			}
			public readonly bool Inverted;
			public int Depth;
			public readonly double Root;
			public readonly double Size;
			#region #method# Equ(Line) 
			public virtual bool Equ(Line Line) {
				return (Line.MX == this.MX && Line.MY == this.MY && Line.EX == this.EX && Line.EY == this.EY);
			}
			#endregion
			#region #method# Rep(A, B) 
			public void Red(Line A, Line B) {
				if(Owner != null) {
					A.Next = B;
					B.Prev = A;
					A.Owner = B.Owner = Owner;
					A.Prev = Prev;
					B.Next = Next;
					if(Prev != null) { Prev.Next = A; } else { Owner.Base = A; }
					if(Next != null) { Next.Prev = B; } else { Owner.Last = B; }
					Prev = null;
					Next = null;
					Owner.Count++;
					Owner = null;
				} else {
					A.Next = B;
					B.Prev = A;
					A.Prev = Prev;
					B.Next = Next;
					if(Prev != null) { Prev.Next = A; }
					if(Next != null) { Next.Prev = B; }
					Prev = null;
					Next = null;
				}
			}
			#endregion
			#region #method# Cut 
			public void Cut() {
				if(Owner != null) {
					if(Prev != null) { Prev.Next = Next; } else { Owner.Base = Next; }
					if(Next != null) { Next.Prev = Prev; } else { Owner.Last = Prev; }
					Prev = null;
					Next = null;
					Owner.Count--;
					Owner = null;
				} else {
					if(Prev != null) { Prev.Next = Next; }
					if(Next != null) { Next.Prev = Prev; }
					Prev = null;
					Next = null;
				}
			}
			#endregion
			#region #method# Red(Path, Count) 
			public void Red(Path Path, ref int Count) {
				Div(0.5, out var BA, out var BB);
				Red(BA, BB);
				var A = Path.Base;
				while(A != null) {
					bool F = false;
					if(A.InIntersect(BA)) { F = true; Count++; }
					if(A.InIntersect(BB)) { F = true; Count++; }
					A = A.Next;
				}
			}
			#endregion
			#region #method# Red 
			public void Red() {
				Div(0.5, out var A, out var B);
				Red(A, B);
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"L Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
			}
			#endregion
			#region #method# Len(Line) 
			public double Len(Line Line) {
				var X = this.X - Line.X;
				var Y = this.Y - Line.Y;
				return System.Math.Sqrt(X * X + Y * Y);
			}
			#endregion
		}
		#endregion
		#region #class# Quadratic 
		public class Quadratic: Line {
			public readonly double QX;
			public readonly double QY;
			#region #method# Equ(Line) 
			public override bool Equ(Line Line) {
				var Q = (Quadratic)Line;
				return (Q.MX == this.MX && Q.MY == this.MY && Q.QX == this.QX && Q.QY == this.QY && Q.EX == this.EX && Q.EY == this.EY);
			}
			#endregion
			#region #method# Div(root, A, B) 
			public override void Div(double root, out Line A, out Line B) {
				var x00 = MX;
				var y00 = MY;
				var x11 = QX;
				var y11 = QY;
				var x22 = EX;
				var y22 = EY;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var x12 = (x22 - x11) * root + x11;
				var y12 = (y22 - y11) * root + y11;
				var x02 = (x12 - x01) * root + x01;
				var y02 = (y12 - y01) * root + y01;
				var S = this.Size * root;
				var ss = this.Size - S;
				if(this.Inverted) {
					A = new Quadratic(x02, y02, x12, y12, x22, y22, this.Inverted, this.Depth + 1, this.Root, ss);
					B = new Quadratic(x00, y00, x01, y01, x02, y02, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					A = new Quadratic(x00, y00, x01, y01, x02, y02, this.Inverted, this.Depth + 1, this.Root, S);
					B = new Quadratic(x02, y02, x12, y12, x22, y22, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #new# (MX, MY, QX, QY, EX, EY, Inverted, Depth, Root, Size) 
			public Quadratic(double MX, double MY, double QX, double QY, double EX, double EY, bool Inverted = false, int Depth = 0, double Root = InitRoot, double Size = InitSize) : base(MX, MY, EX, EY, Inverted, Depth, Root, Size) {
				this.QX = QX;
				this.QY = QY;
			}
			#endregion
			#region #property# Rect 
			public override Rect Rect {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get { if(_Rect == null) _Rect = Rect.From(this); return _Rect; }
			}
			#endregion
			#region #invisible# #get# Pastle 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Pastle {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(MX, MY, QX, QY, EX, EY, Inverted);
			}
			#endregion
			#region #invisible# #get# Return 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Return {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(MX, MY, QX, QY, EX, EY, !Inverted);
			}
			#endregion
			#region #invisible# #get# Invert 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Invert {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Quadratic(EX, EY, QX, QY, MX, MY, !Inverted);
				}
			}
			#endregion
			#region #invisible# #get# Incest 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Incest {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Quadratic(EX, EY, QX, QY, MX, MY, Inverted);
				}
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"Q Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} QX:{QX.ToString(I)} QY:{QY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
			}
			#endregion
		}
		#endregion
		#region #class# Cubic 
		public class Cubic: Line {
			private static readonly double Arc = 4.0 / 3.0 * System.Math.Tan(System.Math.PI * 0.125);
			public readonly double cmX;
			public readonly double cmY;
			public readonly double ceX;
			public readonly double ceY;
			#region #method# Equ(Line) 
			public override bool Equ(Line Line) {
				var Q = (Cubic)Line;
				return (Q.MX == this.MX && Q.MY == this.MY && Q.cmX == this.cmX && Q.cmY == this.cmY && Q.ceX == this.ceX && Q.ceY == this.ceY && Q.EX == this.EX && Q.EY == this.EY);
			}
			#endregion
			#region #method# Div(root, A, B) 
			public override void Div(double root, out Line A, out Line B) {
				var x00 = MX;
				var y00 = MY;
				var x11 = cmX;
				var y11 = cmY;
				var x22 = ceX;
				var y22 = ceY;
				var x33 = EX;
				var y33 = EY;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var x12 = (x22 - x11) * root + x11;
				var y12 = (y22 - y11) * root + y11;
				var x23 = (x33 - x22) * root + x22;
				var y23 = (y33 - y22) * root + y22;
				var x02 = (x12 - x01) * root + x01;
				var y02 = (y12 - y01) * root + y01;
				var x13 = (x23 - x12) * root + x12;
				var y13 = (y23 - y12) * root + y12;
				var x03 = (x13 - x02) * root + x02;
				var y03 = (y13 - y02) * root + y02;
				var S = this.Size * root;
				var ss = this.Size - S;
				if(this.Inverted) {
					A = new Cubic(x03, y03, x13, y13, x23, y23, x33, y33, this.Inverted, this.Depth + 1, this.Root, ss);
					B = new Cubic(x00, y00, x01, y01, x02, y02, x03, y03, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					A = new Cubic(x00, y00, x01, y01, x02, y02, x03, y03, this.Inverted, this.Depth + 1, this.Root, S);
					B = new Cubic(x03, y03, x13, y13, x23, y23, x33, y33, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #new# (MX, MY, cmX, cmY, ceX, ceY, EX, EY, Inverted, Depth, Root, Size) 
			public Cubic(double MX, double MY, double cmX, double cmY, double ceX, double ceY, double EX, double EY, bool Inverted = false, int Depth = 0, double Root = InitRoot, double Size = InitSize) : base(MX, MY, EX, EY, Inverted, Depth, Root, Size) {
				this.cmX = cmX;
				this.cmY = cmY;
				this.ceX = ceX;
				this.ceY = ceY;
			}
			#endregion
			#region #property# Rect 
			public override Rect Rect {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get { if(_Rect == null) _Rect = Rect.From(this); return _Rect; }
			}
			#endregion
			#region #invisible# #get# Pastle 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Pastle {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(MX, MY, cmX, cmY, ceX, ceY, EX, EY, Inverted);
			}
			#endregion
			#region #invisible# #get# Return 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Return {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(MX, MY, cmX, cmY, ceX, ceY, EX, EY, !Inverted);
			}
			#endregion
			#region #invisible# #get# Invert 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Invert {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Cubic(EX, EY, ceX, ceY, cmX, cmY, MX, MY, !Inverted);
				}
			}
			#endregion
			#region #invisible# #get# Incest 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Line Incest {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Cubic(EX, EY, ceX, ceY, cmX, cmY, MX, MY, Inverted);
				}
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"C Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} cmX:{cmX.ToString(I)} cmY:{cmY.ToString(I)} ceX:{ceX.ToString(I)} ceY:{ceY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
			}
			#endregion
		}
		#endregion
		#region #class# Rect 
		public class Rect {
			public readonly double L;
			public readonly double T;
			public readonly double R;
			public readonly double B;
			#region #new# (L, T, R, B) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Rect(double L, double T, double R, double B) {
				this.L = L;
				this.T = T;
				this.R = R;
				this.B = B;
			}
			#endregion
			#region #method# Intersect(X, Y) 
			public bool Intersect(double X, double Y) {
				return (X >= this.L && this.R >= X && Y >= this.T && this.B >= Y);
			}
			#endregion
			#region #method# Intersect(V) 
			public bool Intersect(Rect V) {
				return (V.R >= this.L && this.R >= V.L && V.B >= this.T && this.B >= V.T);
			}
			#endregion
			#region #method# From(Line) 
			public static Rect From(Line Line) {
				var L = Line.MX;
				var T = Line.MY;
				var R = L;
				var B = T;
				var E = Line.EX;
				if(E < L) L = E;
				if(E > R) R = E;
				E = Line.EY;
				if(E < T) T = E;
				if(E > B) B = E;
				return new Rect(L, T, R, B);
			}
			#endregion
			#region #method# From(Quadratic) 
			public static Rect From(Quadratic Quadratic) {
				var L = Quadratic.MX;
				var T = Quadratic.MY;
				var R = L;
				var B = T;
				var E = Quadratic.EX;
				var Q = Quadratic.QX;
				if(E < L) L = E;
				if(Q < L) L = Q;
				if(E > R) R = E;
				if(Q > R) R = Q;
				E = Quadratic.EY;
				Q = Quadratic.QY;
				if(E < T) T = E;
				if(Q < T) T = Q;
				if(E > B) B = E;
				if(Q > B) B = Q;
				return new Rect(L, T, R, B);
			}
			#endregion
			#region #method# From(Cubic) 
			public static Rect From(Cubic Cubic) {
				var L = Cubic.MX;
				var T = Cubic.MY;
				var R = L;
				var B = T;
				var E = Cubic.EX;
				var cm = Cubic.cmX;
				var ce = Cubic.ceX;
				if(E < L) L = E;
				if(cm < L) L = cm;
				if(ce < L) L = ce;
				if(E > R) R = E;
				if(cm > R) R = cm;
				if(ce > R) R = ce;
				E = Cubic.EY;
				cm = Cubic.cmY;
				ce = Cubic.ceY;
				if(E < T) T = E;
				if(cm < T) T = cm;
				if(ce < T) T = ce;
				if(E > B) B = E;
				if(cm > B) B = cm;
				if(ce > B) B = ce;
				return new Rect(L, T, R, B);
			}
			#endregion
			#region #method# ToString 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return "L:" + L.ToString(I) + " T:" + T.ToString(I) + " R:" + R.ToString(I) + " B:" + B.ToString(I);
			}
			#endregion
		}
		#endregion
		#region #method# IntersectL(Aref, Bref, Mlen) 
		public static double IntersectL(ref Line Aref, ref Line Bref, double Mlen = 0.25) {
			var A = Aref.Pastle;
			var B = Bref.Pastle;
			if(A.Intersect(B)) {
				A.C++;
				B.C++;
				var AP = new Path(A);
				var BP = new Path(B);
				do {
					var AC = 0;
					while(A != null && AC < MaxBound) { var N = A.Next; if(A.C > 0) { AC++; A.Red(); } else { A.Cut(); } A = N; }
					if(A != null) {
						A = A.Prev;
						AC = 0;
						var AL = AP.Last;
						while(AL != A) { var N = AL.Prev; if(AL.C > 0 && AC < MaxBound) { AC++; AL.Red(); } else { AL.Cut(); } AL = N; }
					}
					var C = 0;
					var BC = 0;
					while(B != null && BC < MaxBound) { var N = B.Next; if(B.C > 0) { BC++; B.Red(AP, ref C); } else { B.Cut(); } B = N; }
					if(B != null) {
						B = B.Prev;
						BC = 0;
						var BL = BP.Last;
						while(BL != B) { var N = BL.Prev; if(BL.C > 0 && BC < MaxBound) { BC++; BL.Red(AP, ref C); } else { BL.Cut(); } BL = N; }
					}
					A = AP.Base;
					B = BP.Base;
					if(C == 0) break;
				} while(A != null && B != null && (A.Depth <= MaxDepth && B.Depth <= MaxDepth));
				Aref = A;
				Bref = B;
				return A.Len(B);
			}
			return double.NaN;
		}
		#endregion
		#region #method# Intersect(Aref, Bref, Mlen) 
		public static bool Intersect(ref Line Aref, ref Line Bref, double Mlen = 0.25) {
			var A = Aref.Pastle;
			var B = Bref.Pastle;
			if(A.Intersect(B)) {
				A.C++;
				B.C++;
				var AP = new Path(A);
				var BP = new Path(B);
				do {
					var AC = 0;
					while(A != null && AC < MaxBound) { var N = A.Next; if(A.C > 0) { AC++; A.Red(); } else { A.Cut(); } A = N; }
					if(A != null) {
						A = A.Prev;
						AC = 0;
						var AL = AP.Last;
						while(AL != A) { var N = AL.Prev; if(AL.C > 0 && AC < MaxBound) { AC++; AL.Red(); } else { AL.Cut(); } AL = N; }
					}
					var C = 0;
					var BC = 0;
					while(B != null && BC < MaxBound) { var N = B.Next; if(B.C > 0) { BC++; B.Red(AP, ref C); } else { B.Cut(); } B = N; }
					if(B != null) {
						B = B.Prev;
						BC = 0;
						var BL = BP.Last;
						while(BL != B) { var N = BL.Prev; if(BL.C > 0 && BC < MaxBound) { BC++; BL.Red(AP, ref C); } else { BL.Cut(); } BL = N; }
					}
					A = AP.Base;
					B = BP.Base;
					if(C == 0) break;
				} while(A != null && B != null && (A.Depth <= MaxDepth && B.Depth <= MaxDepth));
				if(A != null && B != null && A.Len(B) < Mlen) {
					Aref = A;
					Bref = B;
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
