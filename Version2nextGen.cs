namespace Wholemy {
	public class Bzier {
		public const int MinMaxS = 128;
		public const int MaxDepth = 54;
		public const double InitRoot = 0.0;
		public const double InitSize = 1.0;
		#region #class# Path 
		public class Path {
			public int Count;
			public Line Root;
			public Line Base;
			public Line Last;
			#region #new# (Item) 
			public Path(Line Item) {
				Item.Owner = this;
				Root = Base = Last = Item;
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
			public void Inter(Path P) {
				var I = this.Base;
				while(I != null) {
					if(I.Depth >= 0) {
						var ii = P.Base;
						while(ii != null) {
							if(ii.Depth >= 0) {
								var LT = ii.LenTo(I);
								if(double.IsNaN(I.Len) || LT < I.Len) { I.Len = LT; }
								if(double.IsNaN(ii.Len) || LT < ii.Len) { ii.Len = LT; }
							}
							ii = ii.Next;
						}
					}
					I = I.Next;
				}
				this.Reset();
				P.Reset();
			}
			public Line Get() {
				var I = this.Base;
				if(I != null) {
					var L = I.Len;
					var M = I;
					while(I != null) {
						var ll = I.Len;
						if(ll < L) { L = ll; M = I; }
						I = I.Next;
					}
					return M;
				}
				return null;
			}
			private void Reset() {
				var I = this.Base;
				if(I != null) {
					var L = I.Len;
					var M = I;
					while(I != null) {
						var ll = I.Len;
						if(ll < L) { L = ll; M = I; }
						I = I.Next;
					}
					M.Reset();
				}
			}
			public void Dep() {
				var I = Base;
				while(I != null) { var N = I.Next; I.Red(); I = N; }
			}
			public bool Regen() {
				Line A = null, B = null;
				int C = 0;
				var I = Base;
				var R = false;
				while(I != null) {
					var Next = I.Next;
					if(!I.In) { I.Cut(); }
					I = Next;
				}
				return R;
			}
		}
		#endregion
		#region #class# Dot 
		public class Dot {
			public Dot Prev;
			public Dot Next;
			/// <summary>Длина до другой точки наиболее короткая)</summary>
			public double Len = double.NaN;
			public Dot Led;
			public readonly double Root;
			public readonly double X;
			public readonly double Y;
			#region #new# (X, Y, Root) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot(double Root, double X, double Y) { this.Root = Root; this.X = X; this.Y = Y; }
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"Dot Len:{Len.ToString("G17", I)} Root:{Root.ToString("G17", I)} X:{X.ToString("G17", I)} Y:{Y.ToString("G17", I)}";
			}
			#endregion
			#region #method# LenTo(Dot) 
			public double LenTo(Dot Dot) {
				var X = this.X - Dot.X;
				var Y = this.Y - Dot.Y;
				return System.Math.Sqrt(X * X + Y * Y);
			}
			#endregion
		}
		#endregion
		#region #class# Line 
		public class Line {
			public virtual int Mint => 1;
			public void Reset() {
				var C = 0;
				this.In = true;
				var R = this.Prev;
				while(R != null && R.Depth >= 0) {
					if(C < MinMaxS) {
						R.In = true;
						C++;
					} else {
						R.In = false;
					}
					R = R.Prev;
				}
				C = 0;
				R = this.Next;
				while(R != null && R.Depth >= 0) {
					if(C < MinMaxS) {
						R.In = true;
						C++;
					} else {
						R.In = false;
					}
					R = R.Next;
				}
			}
			public bool In;
			public bool Eq;
			public bool Gin;
			public double Len = double.NaN;
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
				if(this.Inverted) root = 1.0 - root;
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
			#region #method# DivA(root) 
			public virtual Line DivA(double root) {
				if(this.Inverted) root = 1.0 - root;
				var x00 = MX;
				var y00 = MY;
				var x11 = EX;
				var y11 = EY;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var S = this.Size * root;
				var ss = this.Size - S;
				if(this.Inverted) {
					return new Line(x01, y01, x11, y11, this.Inverted, this.Depth + 1, this.Root, ss);
				} else {
					return new Line(x00, y00, x01, y01, this.Inverted, this.Depth + 1, this.Root, S);
				}
			}
			#endregion
			#region #method# DivB(root) 
			public virtual Line DivB(double root) {
				if(this.Inverted) root = 1.0 - root;
				var x00 = MX;
				var y00 = MY;
				var x11 = EX;
				var y11 = EY;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var S = this.Size * root;
				var ss = this.Size - S;
				if(this.Inverted) {
					return new Line(x00, y00, x01, y01, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					return new Line(x01, y01, x11, y11, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #method# Dot(root) 
			public virtual Dot Dot(double root) {
				var R = this.Inverted ? 1.0 - root : root;
				var x00 = MX;
				var y00 = MY;
				var x11 = EX;
				var y11 = EY;
				var x01 = (x11 - x00) * R + x00;
				var y01 = (y11 - y00) * R + y00;
				return new Dot(root, x01, y01);
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
					this.In = true;
					I.In = true;
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
			public readonly bool Inverted;
			public int Depth;
			public readonly double Root;
			public readonly double Size;
			#region #method# Equ(Line) 
			public virtual bool Equ(Line Line) {
				return (Line.MX == this.MX && Line.MY == this.MY && Line.EX == this.EX && Line.EY == this.EY);
			}
			#endregion
			#region #method# Rep(A) 
			public void Rep(Line A) {
				if(Owner != null) {
					A.Owner = Owner;
					A.Prev = Prev;
					A.Next = Next;
					if(Prev != null) { Prev.Next = A; } else { Owner.Base = A; }
					if(Next != null) { Next.Prev = A; } else { Owner.Last = A; }
					Prev = null;
					Next = null;
					Owner = null;
				} else {
					throw new System.InvalidOperationException();
				}
			}
			#endregion
			#region #method# Rep(A, B) 
			public void Rep(Line A, Line B) {
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
					throw new System.InvalidOperationException();
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
					throw new System.InvalidOperationException();
				}
			}
			#endregion
			#region #method# Red(Path, Count) 
			public void Red(Path Path, ref int Count) {
				Div(0.5, out var BA, out var BB);
				Rep(BA, BB);
				var A = Path.Base;
				while(A != null) {
					if(A.Depth >= 0) {
						if(BA.Depth >= 0 && A.InIntersect(BA)) { Count++; }
						if(BB.Depth >= 0 && A.InIntersect(BB)) { Count++; }
					}
					A = A.Next;
				}
			}
			#endregion
			#region #method# Red 
			public void Red() {
				Div(0.5, out var A, out var B);
				Rep(A, B);
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"L {(In ? "In" : "No")} Len:{Len.ToString(I)} Depth:{Depth.ToString(I)} Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
			}
			#endregion
			#region #method# LenTo(Line) 
			public double LenTo(Line Line) {
				var X = this.X - Line.X;
				var Y = this.Y - Line.Y;
				return System.Math.Sqrt(X * X + Y * Y);
			}
			#endregion
		}
		#endregion
		#region #class# Quadratic 
		public class Quadratic: Line {
			public override int Mint => 2;
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
				if(this.Inverted) root = 1.0 - root;
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
			#region #method# DivA(root) 
			public override Line DivA(double root) {
				if(this.Inverted) root = 1.0 - root;
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
					return new Quadratic(x02, y02, x12, y12, x22, y22, this.Inverted, this.Depth + 1, this.Root, ss);
				} else {
					return new Quadratic(x00, y00, x01, y01, x02, y02, this.Inverted, this.Depth + 1, this.Root, S);
				}
			}
			#endregion
			#region #method# DivB(root) 
			public override Line DivB(double root) {
				if(this.Inverted) root = 1.0 - root;
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
					return new Quadratic(x00, y00, x01, y01, x02, y02, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					return new Quadratic(x02, y02, x12, y12, x22, y22, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #method# Dot(root) 
			public override Dot Dot(double root) {
				var R = this.Inverted ? 1.0 - root : root;
				var x00 = MX;
				var y00 = MY;
				var x11 = QX;
				var y11 = QY;
				var x22 = EX;
				var y22 = EY;
				var x01 = (x11 - x00) * R + x00;
				var y01 = (y11 - y00) * R + y00;
				var x12 = (x22 - x11) * R + x11;
				var y12 = (y22 - y11) * R + y11;
				var x02 = (x12 - x01) * R + x01;
				var y02 = (y12 - y01) * R + y01;
				return new Dot(root, x02, y02);
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
				return $"Q {(In ? "In" : "No")} Len:{Len.ToString(I)} Depth:{Depth.ToString(I)} Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} QX:{QX.ToString(I)} QY:{QY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
			}
			#endregion
		}
		#endregion
		#region #class# Cubic 
		public class Cubic: Line {
			public override int Mint => 3;
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
				if(this.Inverted) root = 1.0 - root;
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
			#region #method# DivA(root) 
			public override Line DivA(double root) {
				if(this.Inverted) root = 1.0 - root;
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
					return new Cubic(x03, y03, x13, y13, x23, y23, x33, y33, this.Inverted, this.Depth + 1, this.Root, ss);
				} else {
					return new Cubic(x00, y00, x01, y01, x02, y02, x03, y03, this.Inverted, this.Depth + 1, this.Root, S);
				}
			}
			#endregion
			#region #method# DivB(root) 
			public override Line DivB(double root) {
				if(this.Inverted) root = 1.0 - root;
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
					return new Cubic(x00, y00, x01, y01, x02, y02, x03, y03, this.Inverted, this.Depth + 1, this.Root + ss, S);
				} else {
					return new Cubic(x03, y03, x13, y13, x23, y23, x33, y33, this.Inverted, this.Depth + 1, this.Root + S, ss);
				}
			}
			#endregion
			#region #method# Dot(root) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override Dot Dot(double root) {
				var R = this.Inverted ? 1.0 - root : root;
				var x00 = MX;
				var y00 = MY;
				var x11 = cmX;
				var y11 = cmY;
				var x22 = ceX;
				var y22 = ceY;
				var x33 = EX;
				var y33 = EY;
				var x01 = (x11 - x00) * R + x00;
				var y01 = (y11 - y00) * R + y00;
				var x12 = (x22 - x11) * R + x11;
				var y12 = (y22 - y11) * R + y11;
				var x23 = (x33 - x22) * R + x22;
				var y23 = (y33 - y22) * R + y22;
				var x02 = (x12 - x01) * R + x01;
				var y02 = (y12 - y01) * R + y01;
				var x13 = (x23 - x12) * R + x12;
				var y13 = (y23 - y12) * R + y12;
				var x03 = (x13 - x02) * R + x02;
				var y03 = (y13 - y02) * R + y02;
				return new Dot(root, x03, y03);
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
				return $"C {(In ? "In" : "No")} Len:{Len.ToString(I)} Depth:{Depth.ToString(I)} Root:{Root.ToString(I)} Size:{Size.ToString(I)} MX:{MX.ToString(I)} MY:{MY.ToString(I)} cmX:{cmX.ToString(I)} cmY:{cmY.ToString(I)} ceX:{ceX.ToString(I)} ceY:{ceY.ToString(I)} EX:{EX.ToString(I)} EY:{EY.ToString(I)}";
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
			var D = 0;
			if(A.Intersect(B)) {
				A.In = B.In = true;
				var AP = new Path(A);
				var BP = new Path(B);
				do {
					var AC = 0;
					while(A != null && AC < MinMaxS) { var N = A.Next; if(A.In) { AC++; A.Red(); } else { A.Cut(); } A = N; }
					if(A != null) {
						A = A.Prev;
						AC = 0;
						var AL = AP.Last;
						while(AL != A) { var N = AL.Prev; if(AL.In && AC < MinMaxS) { AC++; AL.Red(); } else { AL.Cut(); } AL = N; }
					}
					var C = 0;
					var BC = 0;
					while(B != null && BC < MinMaxS) { var N = B.Next; if(B.In) { BC++; B.Red(AP, ref C); } else { B.Cut(); } B = N; }
					if(B != null) {
						B = B.Prev;
						BC = 0;
						var BL = BP.Last;
						while(BL != B) { var N = BL.Prev; if(BL.In && BC < MinMaxS) { BC++; BL.Red(AP, ref C); } else { BL.Cut(); } BL = N; }
					}
					A = AP.Base;
					B = BP.Base;
					D++;
					if(C == 0) break;
				} while(A != null && B != null && D < MaxDepth);
				if(A != null && !A.In) A = A.Next;
				if(B != null && !B.In) B = B.Next;
				if(A != null && B != null) {
					Aref = A;
					Bref = B;
					return A.LenTo(B);
				}
			}
			return double.NaN;
		}
		#endregion
		#region #class# Min 
		public class Min {
			public Line GetA(double Mlen) {
				var I = this.BaseA;
				if(I != null) {
					var L = I.Len;
					var M = I;
					while(I != null) {
						if(I.Len < Mlen) { return LineA.DivB(I.Root).DivA(0.0); }
						I = I.Next;
					}
				}
				return null;
			}
			public Line GetB(double Mlen) {
				var I = this.BaseB;
				if(I != null) {
					var L = I.Len;
					var M = I;
					while(I != null) {
						if(I.Len < Mlen) { return LineB.DivB(I.Root).DivA(0.0); }
						I = I.Next;
					}
				}
				return null;
			}
			public double Size;
			public readonly Line LineA;
			public readonly Line LineB;
			public Dot BaseA;
			public Dot LastA;
			public int CountA;
			public Dot[] CacheA;
			public Dot BaseB;
			public Dot LastB;
			public int CountB;
			public Dot[] CacheB;
			#region #property# Dots 
			public Dot[] ItemsA {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if(CacheA != null) return CacheA;
					var I = CountA;
					var A = new Dot[I];
					var S = LastA;
					while(--I >= 0) {
						A[I] = S;
						S = S.Prev;
					}
					CacheA = A;
					return A;
				}
			}
			public Dot[] ItemsB {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if(CacheB != null) return CacheB;
					var I = CountB;
					var B = new Dot[I];
					var S = LastB;
					while(--I >= 0) {
						B[I] = S;
						S = S.Prev;
					}
					CacheB = B;
					return B;
				}
			}
			#endregion
			public Min(Line A, Line B) {
				this.LineA = A;
				this.LineB = B;
				Add(0.0);
				//Add(0.5);
				//Size = 0.5;
				var S = 1.0 / MinMaxS;
				Size = S;
				var I = S;
				while(I < 1.0) {
					Add(I);
					I += S;
				}
				Add(1.0);
			}
			public void Add(double root) {
				AddA(root);
				AddB(root);
			}
			public void AddA(double root) {
				var A = this.LineA.Dot(root);
				Dot Prev = null;
				var Next = BaseA;
				while(Next != null) {
					if(Next.Root == root) { return; }
					if(Next.Root > root) { break; }
					Prev = Next; Next = Next.Next;
				}
				if(Prev == null) { BaseA = A; } else { Prev.Next = A; A.Prev = Prev; }
				if(Next == null) { LastA = A; } else { Next.Prev = A; A.Next = Next; }
				CountA++;
				CacheA = null;
			}
			public void AddB(double root) {
				var B = this.LineB.Dot(root);
				Dot Prev = null;
				var Next = BaseB;
				while(Next != null) {
					if(Next.Root == root) { return; }
					if(Next.Root > root) { break; }
					Prev = Next; Next = Next.Next;
				}
				if(Prev == null) { BaseB = B; } else { Prev.Next = B; B.Prev = Prev; }
				if(Next == null) { LastB = B; } else { Next.Prev = B; B.Next = Next; }
				CountB++;
				CacheA = null;
			}
			public void CutA(Dot Dot) {
				var P = Dot.Prev;
				var N = Dot.Next;
				if(P != null) { P.Next = N; } else { BaseA = N; }
				if(N != null) { N.Prev = P; } else { LastA = P; }
				Dot.Prev = Dot.Next = null;
				CountA--;
				CacheA = null;
			}
			public void CutB(Dot Dot) {
				var P = Dot.Prev;
				var N = Dot.Next;
				if(P != null) { P.Next = N; } else { BaseB = N; }
				if(N != null) { N.Prev = P; } else { LastB = P; }
				Dot.Prev = Dot.Next = null;
				CountB--;
				CacheB = null;
			}
			public void Dep() {
				this.Size *= 0.5;
				RepA();
				RepB();
			}
			public void Int() {
				Len();
				AntA();
				AntB();
			}
			public void Len() {
				var I = BaseA;
				while(I != null) {
					var ii = BaseB;
					while(ii != null) {
						var LT = ii.LenTo(I);
						if(double.IsNaN(I.Len) || LT < I.Len) { I.Len = LT; I.Led = ii; }
						if(double.IsNaN(ii.Len) || LT < ii.Len) { ii.Len = LT; ii.Led = I; }
						ii = ii.Next;
					}
					I = I.Next;
				}
			}
			public void AntA() {
				var Dots = BaseA;
				if(Dots != null) {
					var M = Dots;
					var L = Dots.Len;
					Dots = Dots.Next;
					while(Dots != null) {
						var ll = Dots.Len;
						if(ll < L) { L = ll; M = Dots; }
						Dots = Dots.Next;
					}
					var C = MinMaxS;
					var R = M.Prev;
					while(R != null) {
						var N = R.Prev;
						if(C > 0) {
							C--;
						} else {
							CutA(R);
						}
						R = N;
					}
					if(C > 0) {
						R = BaseA;
						var rr = R.Root;
						while(C > 0 && rr > 0.0) {
							rr = rr - Size;
							if(rr < 0.0) rr = 0.0;
							AddA(rr);
							C--;
						}
					}
					C = MinMaxS;
					R = M.Next;
					while(R != null) {
						var N = R.Next;
						if(C > 0) {
							C--;
						} else {
							CutA(R);
						}
						R = N;
					}
					if(C > 0) {
						R = LastA;
						var rr = R.Root;
						while(C > 0 && rr < 1.0) {
							rr = rr + Size;
							if(rr > 1.0) rr = 1.0;
							AddA(rr);
							C--;
						}
					}
				}
			}
			public void AntB() {
				var Dots = BaseB;
				if(Dots != null) {
					var M = Dots;
					var L = Dots.Len;
					Dots = Dots.Next;
					while(Dots != null) {
						var ll = Dots.Len;
						if(ll < L) { L = ll; M = Dots; }
						Dots = Dots.Next;
					}
					var C = MinMaxS;
					var R = M.Prev;
					while(R != null) {
						var N = R.Prev;
						if(C > 0) {
							C--;
						} else {
							CutB(R);
						}
						R = N;
					}
					if(C > 0) {
						R = BaseB;
						var rr = R.Root;
						while(C > 0 && rr > 0.0) {
							rr = rr - Size;
							if(rr < 0.0) rr = 0.0;
							AddB(rr);
							C--;
						}
					}
					C = MinMaxS;
					R = M.Next;
					while(R != null) {
						var N = R.Next;
						if(C > 0) {
							C--;
						} else {
							CutB(R);
						}
						R = N;
					}
					if(C > 0) {
						R = LastB;
						var rr = R.Root;
						while(C > 0 && rr < 1.0) {
							rr = rr + Size;
							if(rr > 1.0) rr = 1.0;
							AddB(rr);
							C--;
						}
					}
				}
			}
			public void RepA() {
				var I = BaseA;
				if(I != null) {
					var S = Size;
					while(I != null) {
						var N = I.Next;
						if(N != null) {
							var R = (N.Root - I.Root) * 0.5;
							if(I.Root + R < N.Root) {
								AddA(I.Root + R);
							}
						}
						I = N;
					}
				}
			}
			public void RepB() {
				var I = BaseB;
				if(I != null) {
					var S = Size;
					while(I != null) {
						var N = I.Next;
						if(N != null) {
							var R = (N.Root - I.Root) * 0.5;
							if(I.Root + R < N.Root) {
								AddB(I.Root + R);
							}
						}
						I = N;
					}
				}
			}
		}
		#endregion
		#region #method# Intersect2L(Aref, Bref, Mlen) 
		/// <remarks>
		/// Поиск пересечений работает следующим образом, сначала находится наименьший минимум, одно пересечение в одной кривой,
		/// затем кривая делится в области этого пересечения и в двух отрезках происходит поиск максимумов среди минимумов,
		/// кривые делятся в области максимумов на отрезки и среди них происходит поиск минимумов, пересечений)
		/// </remarks>
		public static double Intersect2L(ref Line Aref, ref Line Bref, double Mlen = 0.25) {
			var A = Aref.Pastle;
			var B = Bref.Pastle;
			var D = 0;
			var M = Mlen;
			if(A.Intersect(B)) {
				var Mint = A.Mint * B.Mint;
				var Min = new Min(A, B);
				do {
					Min.Dep();
					Min.Int();
					D++;
				} while(D < MaxDepth);

				A = Min.GetA(Mlen);
				B = Min.GetB(Mlen);
				if(A != null && B != null) {
					Aref = A;
					Bref = B;
					return A.LenTo(B);
				}
			}
			return double.NaN;
		}
		#endregion
		#region #method# Intersect(Aref, Bref, Mlen) 
		public static bool Intersect(ref Line Aref, ref Line Bref, double Mlen = 0.25) {

			return false;
		}
		#endregion
	}
}
