namespace Wholemy {
	#region #class# Inline 
	public class Inline {
		public const int MaxDepth = 64;
		#region #class# Quadratic 
		public class Quadratic : Inline {
			public readonly double x2;
			public readonly double y2;
			private readonly double ax2;
			private readonly double ay2;
			private readonly double bx2;
			private readonly double by2;
			#region #new# (x0, y0, x1, y1, x2, y2, S = 0.5, I = 0.5, O = null) 
			public Quadratic(double x0, double y0, double x1, double y1, double x2, double y2, double S = 0.5, double I = 0.5, Inline O = null, bool Not = false) : base(O, S, I, x0, y0, x2, y2, Not) {
				this.x2 = x1;
				this.y2 = y1;
				var v = x0;
				if (x1 < v) v = x1;
				if (x2 < v) v = x2;
				L = v;
				v = y0;
				if (y1 < v) v = y1;
				if (y2 < v) v = y2;
				T = v;
				v = x0;
				if (x1 > v) v = x1;
				if (x2 > v) v = x2;
				R = v;
				v = y0;
				if (y1 > v) v = y1;
				if (y2 > v) v = y2;
				B = v;
				var x01 = (x1 - x0) * 0.5 + x0;
				var y01 = (y1 - y0) * 0.5 + y0;
				var x12 = (x2 - x1) * 0.5 + x1;
				var y12 = (y2 - y1) * 0.5 + y1;
				this.X = (x12 - x01) * 0.5 + x01;
				this.Y = (y12 - y01) * 0.5 + y01;
				this.bx2 = x01; this.by2 = y01;
				this.ax2 = x12; this.ay2 = y12;
			}
			#endregion
			#region #override# #method# Div(root, b0, b1) 
			public override void Div(double root, out Inline b0, out Inline b1) {
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x1;
				var y22 = y1;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var x12 = (x22 - x11) * root + x11;
				var y12 = (y22 - y11) * root + y11;
				var x02 = (x12 - x01) * root + x01;
				var y02 = (y12 - y01) * root + y01;
				b0 = new Quadratic(x00, y00, x01, y01, x02, y02);
				b1 = new Quadratic(x02, y02, x12, y12, x22, y22);
			}
			#endregion
			#region #override# #method# Get(root, X, Y) 
			public override void Get(double root, out double X, out double Y) {
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x1;
				var y22 = y1;
				var x01 = (x11 - x00) * root + x00;
				var y01 = (y11 - y00) * root + y00;
				var x12 = (x22 - x11) * root + x11;
				var y12 = (y22 - y11) * root + y11;
				var x02 = (x12 - x01) * root + x01;
				var y02 = (y12 - y01) * root + y01;
				X = x02;
				Y = y02;
			}
			#endregion
			#region #invisible# #get# New 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline New {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(x0, y0, x2, y2, x1, y1, Not: this.Not);
			}
			#endregion
			#region #invisible# #get# NewNot 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline NewNot {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(x0, y0, x2, y2, x1, y1, Not: !this.Not);
			}
			#endregion
			#region #property# Below 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline Below {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.belowb == null) {
						var S = 0.5 * this.Size;
						if (this.Not) {
							this.belowb = new Quadratic(X, Y, this.ax2, this.ay2, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
						} else {
							this.belowb = new Quadratic(this.x0, this.y0, this.bx2, this.by2, X, Y, S, this.Root - S, this, this.Not);
						}
					}
					return this.belowb;
				}
			}
			#endregion
			#region #property# Above 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline Above {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.aboveb == null) {
						var S = 0.5 * this.Size;
						if (this.Not) {
							this.aboveb = new Quadratic(this.x0, this.y0, this.bx2, this.by2, X, Y, S, this.Root - S, this, this.Not);
						} else {
							this.aboveb = new Quadratic(X, Y, this.ax2, this.ay2, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
						}
					}
					return this.aboveb;
				}
			}
			#endregion
		}
		#endregion
		#region #class# Cubic 
		public class Cubic : Inline {
			public readonly double x2;
			public readonly double y2;
			public readonly double x3;
			public readonly double y3;
			private readonly double ax2;
			private readonly double ay2;
			private readonly double ax3;
			private readonly double ay3;
			private readonly double bx2;
			private readonly double by2;
			private readonly double bx3;
			private readonly double by3;
			#region #new# (x0, y0, x1, y1, x2, y2, x3, y3, S = 0.5, I = 0.5, O = null) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Cubic(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, double S = 0.5, double I = 0.5, Inline O = null, bool Not = false) : base(O, S, I, x0, y0, x3, y3, Not) {
				this.x2 = x1;
				this.y2 = y1;
				this.x3 = x2;
				this.y3 = y2;
				var v = x0;
				if (x1 < v) v = x1;
				if (x2 < v) v = x2;
				if (x3 < v) v = x3;
				L = v;
				v = y0;
				if (y1 < v) v = y1;
				if (y2 < v) v = y2;
				if (y3 < v) v = y3;
				T = v;
				v = x0;
				if (x1 > v) v = x1;
				if (x2 > v) v = x2;
				if (x3 > v) v = x3;
				R = v;
				v = y0;
				if (y1 > v) v = y1;
				if (y2 > v) v = y2;
				if (y3 > v) v = y3;
				B = v;
				var x01 = (x1 - x0) * 0.5 + x0;
				var y01 = (y1 - y0) * 0.5 + y0;
				var x12 = (x2 - x1) * 0.5 + x1;
				var y12 = (y2 - y1) * 0.5 + y1;
				var x23 = (x3 - x2) * 0.5 + x2;
				var y23 = (y3 - y2) * 0.5 + y2;
				var x02 = (x12 - x01) * 0.5 + x01;
				var y02 = (y12 - y01) * 0.5 + y01;
				var x13 = (x23 - x12) * 0.5 + x12;
				var y13 = (y23 - y12) * 0.5 + y12;
				this.X = (x13 - x02) * 0.5 + x02;
				this.Y = (y13 - y02) * 0.5 + y02;
				this.bx2 = x01; this.by2 = y01; this.bx3 = x02; this.by3 = y02;
				this.ax2 = x13; this.ay2 = y13; this.ax3 = x23; this.ay3 = y23;
			}
			#endregion
			#region #override# #method# Div(root, b0, b1) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override void Div(double root, out Inline b0, out Inline b1) {
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x3;
				var y22 = y3;
				var x33 = x1;
				var y33 = y1;
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
				b0 = new Cubic(x00, y00, x01, y01, x02, y02, x03, y03);
				b1 = new Cubic(x03, y03, x13, y13, x23, y23, x33, y33);
			}
			#endregion
			#region #override# #method# Get(root, X, Y) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override void Get(double root, out double X, out double Y) {
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x3;
				var y22 = y3;
				var x33 = x1;
				var y33 = y1;
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
				X = x03;
				Y = y03;
			}
			#endregion
			#region #invisible# #get# New 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline New {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(x0, y0, x2, y2, x3, y3, x1, y1, Not: this.Not);
			}
			#endregion
			#region #invisible# #get# NewNot 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline NewNot {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(x0, y0, x2, y2, x3, y3, x1, y1, Not: !this.Not);
			}
			#endregion
			#region #property# Below 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline Below {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.belowb == null) {
						var S = 0.5 * this.Size;
						if (this.Not) {
							this.belowb = new Cubic(X, Y, this.ax2, this.ay2, this.ax3, this.ay3, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
						} else {
							this.belowb = new Cubic(this.x0, this.y0, this.bx2, this.by2, this.bx3, this.by3, X, Y, S, this.Root - S, this, this.Not);
						}
					}
					return this.belowb;
				}
			}
			#endregion
			#region #property# Above 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Inline Above {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.aboveb == null) {
						var S = 0.5 * this.Size;
						if (this.Not) {
							this.aboveb = new Cubic(this.x0, this.y0, this.bx2, this.by2, this.bx3, this.by3, X, Y, S, this.Root - S, this, this.Not);
						} else {
							this.aboveb = new Cubic(X, Y, this.ax2, this.ay2, this.ax3, this.ay3, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
						}
					}
					return this.aboveb;
				}
			}
			#endregion
		}
		#endregion
		public readonly double Size;
		private double L;
		private double T;
		private double R;
		private double B;
		public readonly double x0;
		public readonly double y0;
		public readonly double x1;
		public readonly double y1;
		public readonly double Root;
		public double X;
		public double Y;
		public readonly int Depth;
		private readonly Inline Parent;
		#region #property# RootBelow 
		public double RootBelow {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => this.Root - this.Size;
		}
		#endregion
		#region #property# RootAbove 
		public double RootAbove {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => this.Root + this.Size;
		}
		#endregion
		#region #property# DepthAbove 
		public Inline DepthAbove {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var p = this.Parent;
				var t = this;
				if (p == null) return null;
				var d = this.Depth - 1;
				while (p != null && p.aboveb == t) { t = p; p = p.Parent; }
				while (p != null) {
					if (d == p.Depth) return p.Above;
					p = p.Above;
				}
				return null;
			}
		}
		#endregion
		#region #property# DepthBelow 
		public Inline DepthBelow {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var p = this.Parent;
				var t = this;
				if (p == null) return null;
				var d = this.Depth - 1;
				while (p != null && p.belowb == t) { t = p; p = p.Parent; }
				while (p != null) {
					if (d == p.Depth) return p.Below;
					p = p.Below;
				}
				return null;
			}
		}
		#endregion
		#region #property# Below 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		private Inline belowb;
		public virtual Inline Below {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.belowb == null) {
					var S = 0.5 * this.Size;
					if (this.Not) {
						this.belowb = new Inline(this.X, this.Y, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
					} else {
						this.belowb = new Inline(this.x0, this.y0, this.X, this.Y, S, this.Root - S, this, this.Not);
					}
				}
				return this.belowb;
			}
		}
		#endregion
		#region #property# Above 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		private Inline aboveb;
		public virtual Inline Above {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.aboveb == null) {
					var S = 0.5 * this.Size;
					if (this.Not) {
						this.aboveb = new Inline(this.x0, this.y0, this.X, this.Y, S, this.Root - S, this, this.Not);
					} else {
						this.aboveb = new Inline(this.X, this.Y, this.x1, this.y1, this.Size - S, this.Root + S, this, this.Not);
					}
				}
				return this.aboveb;
			}
		}
		#endregion
		#region #property# OtherAbove 
		public Inline OtherAbove {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var X = this.X;
				var Y = this.Y;
				var T = this.DepthAbove;
				while (T != null && T.X == X && T.Y == Y) { T = T.DepthAbove; }
				return T;
			}
		}
		#endregion
		#region #property# OtherBelow 
		public Inline OtherBelow {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var X = this.X;
				var Y = this.Y;
				var T = this.DepthBelow;
				while (T != null && T.X == X && T.Y == Y) { T = T.DepthBelow; }
				return T;
			}
		}
		#endregion
		#region #property# MaxBelow 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Inline MaxBelow {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var A = this; while (A.Depth < MaxDepth) { A = A.Below; }
				return A;
			}
		}
		#endregion
		#region #property# MaxAbove 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Inline MaxAbove {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				var A = this; while (A.Depth < MaxDepth) { A = A.Above; }
				return A;
			}
		}
		#endregion
		#region #method# OtherGreater(B) 
		public Inline OtherGreater(Inline B) {
			var OA = this.OtherAbove;
			if (OA != null && OA.Greater(this, B)) return OA;
			var OB = this.OtherBelow;
			if (OB != null && OB.Greater(this, B)) return OB;
			return null;
		}
		#endregion
		#region #method# Greater(a, b) 
		/// <summary>Возвращает истину, если X и Y этой линии больше чем A и меньше или равно B)</summary>
		public bool Greater(Inline a, Inline b, bool equ = false) {
			var aX = a.X;
			var aY = a.Y;
			var bX = b.X;
			var bY = b.Y;
			var tX = this.X;
			var tY = this.Y;
			bool x;
			bool xb = equ;
			if (aX < bX) {
				x = (tX >= aX && tX <= bX);
				if (!equ) xb = tX > aX;
			} else if (aX > bX) {
				x = (tX >= bX && tX <= aX);
				if (!equ) xb = tX < aX;
			} else {
				x = tX == bX;
			}
			if (aY < bY) {
				if (tY >= aY && tY <= bY && (x && (tY > aY || xb))) return true;
			} else if (aY > bY) {
				if (tY >= bY && tY <= aY && (x && (tY < aY || xb))) return true;
			} else {
				if (tY == bY && x && xb) return true;
			}
			return false;
		}
		#endregion
		#region #new# (O, S, I, x0, y0, x1, y1) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private Inline(Inline O, double S, double I, double x0, double y0, double x1, double y1, bool Not) {
			this.Parent = O;
			this.Size = S;
			this.Root = I;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
			this.Not = Not;
		}
		#endregion
		#region #new# (x0, y0, x1, y1, S = 0.5, I = 0.5, O = null) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public Inline(double x0, double y0, double x1, double y1, double S = 0.5, double I = 0.5, Inline O = null, bool Not = false) {
			this.Parent = O;
			this.Size = S;
			this.Root = I;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
			this.X = (x1 - x0) * 0.5 + x0;
			this.Y = (y1 - y0) * 0.5 + y0;
			this.Not = Not;
			if (x0 < x1) { L = x0; R = x1; } else { L = x1; R = x0; }
			if (y0 < y1) { T = y0; B = y1; } else { T = y1; B = y0; }
		}
		#endregion
		#region #virtual# #method# Div(root, b0, b1) 
		public virtual void Div(double root, out Inline b0, out Inline b1) {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * root + x00;
			var y01 = (y11 - y00) * root + y00;
			b0 = new Inline(x00, y00, x01, y01);
			b1 = new Inline(x01, y01, x11, y11);
		}
		#endregion
		#region #virtual# #method# Get(root, X, Y) 
		public virtual void Get(double root, out double X, out double Y) {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * root + x00;
			var y01 = (y11 - y00) * root + y00;
			X = x01;
			Y = y01;
		}
		#endregion
		#region #method# Len(a) 
		public double Len(Inline a) {
			var x1 = this.X - a.X;
			var y1 = this.Y - a.Y;
			return System.Math.Sqrt(x1 * x1 + y1 * y1);
		}
		#endregion
		#region #method# Bet 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		private Between between;
		public readonly bool Not;
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private Between Bet() {
			if (this.between == null) return this.between = new Between(this);
			return this.between;
		}
		#endregion
		#region #class# Between 
		private class Between {
			#region #method# Bet 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Between between;
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Between Bet() {
				if (this.between == null) return this.between = new Between(this);
				return this.between;
			}
			#endregion
			public readonly Inline[] A;
			#region #new# (Inline a) 
			public Between(Inline a) {
				#region #debug# 
#if DEBUG
				if (a.between != null) throw new System.InvalidOperationException();
#endif
				#endregion
				a.between = this;
				this.A = new Inline[] { a.Below, a.Above };
			}
			#endregion
			#region #new# (Between a) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Between(Between a) {
				#region #debug# 
#if DEBUG
				if (a.between != null) throw new System.InvalidOperationException();
#endif
				#endregion
				a.between = this;
				var aa = a.A;
				var ll = aa.Length;
				var l = ll * 2;
				var A = new Inline[l];
				var I = 0;
				for (var i = 0; i < ll; i++) {
					var aai = aa[i];
					A[I++] = aai.Below;
					A[I++] = aai.Above;
				}
				this.A = A;
			}
			#endregion
			#region #method# Intersect(B) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public bool Intersect(Between B) {
				var A = this.A;
				var al = A.Length;
				var ba = B.A;
				var bl = ba.Length;
				for (var ai = 0; ai < al; ai++) {
					var a = A[ai];
					for (var bi = 0; bi < bl; bi++) {
						var b = ba[bi];
						if (a.Intersect(b)) { return true; }
					}
				}
				return false;
			}
			#endregion
			#region #method# Intersect(aa, bb, B) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public bool Intersect(ref Inline aa, ref Inline bb, Between B) {
				var A = this.A;
				var al = A.Length;
				var ba = B.A;
				var bl = ba.Length;
				for (var ai = 0; ai < al; ai++) {
					var a = A[ai];
					for (var bi = 0; bi < bl; bi++) {
						var b = ba[bi];
						if (a.Intersect(b)) { aa = a; bb = b; return true; }
					}
				}
				return false;
			}
			#endregion
		}
		#endregion
		#region #method# Intersect(a) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private bool Intersect(Inline a) {
			return (a.R >= this.L && this.R >= a.L && a.B >= this.T && this.B >= a.T);
		}
		#endregion
		#region #class# Chance 
		private class Chance {
			public readonly int Cout;
			public readonly Inline Line;
			public readonly Chance Next;
			private Chance Prev;
			public bool ExistsBelow;
			public bool ExistsAbove;
			#region #new# (Next, Line) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			private Chance(Chance Next, Inline Line) {
				this.Line = Line;
				this.Next = Next;
				this.Cout = (Next != null) ? Next.Cout + 1 : 1;
				if (Next != null) {
					this.Prev = Next.Prev;
					Next.Prev = this;
				} else {
					this.Prev = this;
				}
			}
			#endregion
			#region #operator# + (Next, Line) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public static Chance operator +(Chance Next, Inline Line) {
				return new Chance(Next, Line);
			}
			#endregion
			#region #implicit operator# (Line)
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public static implicit operator Chance(Inline Line) {
				return new Chance(null, Line);
			}
			#endregion
			#region #method# Test(cb) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public void Test(Chance cb) {
				var A = Line;
				var B = cb.Line;
				var AB = A.Below;
				var AA = A.Above;
				var BB = B.Below;
				var BA = B.Above;
				var ABBB = AB.Intersect(BB);
				var ABBA = AB.Intersect(BA);
				var AABB = AA.Intersect(BB);
				var AABA = AA.Intersect(BA);
				if (ABBB || ABBA) { this.ExistsBelow = true; }
				if (AABB || AABA) { this.ExistsAbove = true; }
				if (AABB || ABBB) { cb.ExistsBelow = true; }
				if (AABA || ABBA) { cb.ExistsAbove = true; }
			}
			#endregion
			#region #method# Exists 
			public Chance Exists(int bound) {
				Chance R = null;
				Inline L;
				var C = this.Prev;
				var M = this.Cout - bound;
				while (C != this) {
					if (bound < 4 || C.Cout < bound || C.Cout > M) {
						L = C.Line;
						if (C.ExistsAbove) { R += L.Above; }
						if (C.ExistsBelow) { R += L.Below; }
					}
					C = C.Prev;
				}
				L = this.Line;
				if (this.ExistsAbove) { R += L.Above; }
				if (this.ExistsBelow) { R += L.Below; }
				return R;
			}
			#endregion
			public override string ToString() {
				return $"Cout = {Cout}";
			}
		}
		#endregion
		#region #method# Intersect(b, depth) 
		private bool Intersect(Inline b, int depth, int bound) {
			if (this.Intersect(b)) {
				if (depth > 0) {
					Chance CA = this;
					Chance CB = b;
					while (--depth >= 0) {
						Chance TA = CA;
						while (TA != null) {
							Chance TB = CB;
							while (TB != null) {
								var TBL = TB.Line;
								TA.Test(TB);
								TB = TB.Next;
							}
							TA = TA.Next;
						}
						CA = CA.Exists(bound);
						CB = CB.Exists(bound);
						if (CA == null || CB == null) return false;
					}
				}
				return true;
			}
			return false;
			//if (this.Intersect(b)) {
			//	if (depth > 0) {
			//		var aa = this.Bet();
			//		var bb = b.Bet();
			//		if (!aa.Intersect(bb)) return false;
			//		while (--depth > 0) {
			//			aa = aa.Bet();
			//			bb = bb.Bet();
			//			if (!aa.Intersect(bb)) return false;
			//		}
			//	}
			//	return true;
			//}
			//return false;
		}
		#endregion
		#region #method# IntersectEnd(Aref, Bref, depth) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private static int IntersectEnd(ref Inline Aref, ref Inline Bref, int depth) {
			var A = Aref;
			var B = Bref;
			var C = 0;
			if (A.Intersect(B)) {
				if (depth > 0) {
					var aa = A.Bet();
					var bb = B.Bet();
					if (aa.Intersect(ref A, ref B, bb)) {
						Aref = A;
						Bref = B;
						C++;
					}
					while (--depth > 0) {
						aa = aa.Bet();
						bb = bb.Bet();
						if (aa.Intersect(ref A, ref B, bb)) {
							Aref = A.Parent;
							Bref = B.Parent;
							C++;
						}
					}
				}
			}
			return C;
		}
		#endregion

		#region #method# Intersect(Aref, Bref, Aend, Bnot, Lmin, Dmin, Dmax) 
		/// <summary>Возвращает истину если инлайны пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый инлайн)</param>
		/// <param name="Bref">Второй инлайн)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="Dmin">Минимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="bound">Ограничитель разбора при заглублении)</param>
		/// <returns>Возвращает истину если инлайны пересекаются или ложь)</returns>
		public static bool Intersect(ref Inline Aref, ref Inline Bref, bool Aend = false, bool Bnot = false, double Lmin = 0.1, int Dmin = 7, int Dmax = 12, int bound = 20) {
			bool O;
			var A = Aref.New;
			if (Aend) A = A.NewNot;
			var B = Bref.New;
			if (Aend ^ Bnot) B = B.NewNot;
			var depth = Dmin;
			var Abak = A;
			var Bbak = B;
			Inline AB, AA, BB, BA;
			if (A.Intersect(B)) {
			Next:
				if (A.Depth < MaxDepth && B.Depth < MaxDepth) {
					AB = A.Below; AA = A.Above; BB = B.Below; BA = B.Above;
					var ABB = AB.Intersect(B, depth, bound);
					var AAB = AA.Intersect(B, depth, bound);
					if (AAB && !ABB) { A = AA; goto Next; }
					if (ABB && !AAB) { A = AB; goto Next; }
					var BBA = BB.Intersect(A, depth, bound);
					var BAA = BA.Intersect(A, depth, bound);
					if (BAA && !BBA) { B = BA; goto Next; }
					if (BBA && !BAA) { B = BB; goto Next; }

					if (ABB && AAB) { A = AB; goto Next; }
					if (BBA && BAA) { B = BB; goto Next; }
				}
				//int C;
				//do { C = IntersectEnd(ref A, ref B, depth); } while (C > 1 && A.Depth < MaxDepth && B.Depth < MaxDepth);
				var AS = A;
				do {
					O = false;
					AA = AS.OtherGreater(B);
					if (AA != null) {
						AS = AA; O = true;
					}
				} while (O);
				do {
					O = false;
					AB = AS.Above;
					AA = AS.Below;
					var abl = AB.Len(B);
					var aal = AA.Len(B);
					if (abl < aal) { AS = AB; O = true; }
					if (abl > aal) { AS = AA; O = true; }
				} while (O);
				var BS = B;
				do {
					O = false;
					BB = BS.OtherGreater(A);
					if (BB != null) {
						BS = BB; O = true;
					}
				} while (O);
				do {
					O = false;
					BB = BS.Above;
					BA = BS.Below;
					var bbl = BB.Len(A);
					var bal = BA.Len(A);
					if (bbl < bal) { BS = BB; O = true; }
					if (bbl > bal) { BS = BA; O = true; }
				} while (O);
				var ASS = A;
				var BSS = B;
				do {
					O = false;
					AA = ASS.OtherGreater(BSS);
					if (AA != null) {
						ASS = AA; O = true;
					}
					BB = BSS.OtherGreater(ASS);
					if (BB != null) {
						BSS = BB; O = true;
					}
				} while (O);
				do {
					O = false;
					AB = ASS.Above;
					AA = ASS.Below;
					BB = BSS.Above;
					BA = BSS.Below;
					var abl = AB.Len(BSS);
					var aal = AA.Len(BSS);
					var bbl = BB.Len(ASS);
					var bal = BA.Len(ASS);
					var al = abl < aal ? abl : aal;
					var bl = bbl < bal ? bbl : bal;
					if (al < bl || bbl == bal) {
						if (abl < aal) { ASS = AB; O = true; }
						if (abl > aal) { ASS = AA; O = true; }
					}
					if (al > bl || abl == aal) {
						if (bbl < bal) { BSS = BB; O = true; }
						if (bbl > bal) { BSS = BA; O = true; }
					}
				} while (O);
				var SSL = ASS.Len(BSS);
				var ASL = AS.Len(B);
				var BSL = BS.Len(A);
				if (SSL < ASL && SSL < BSL) {
					if (SSL < Lmin) {
						Aref = ASS;
						Bref = BSS;
						return true;
					}
				} else if (ASL < BSL) {
					if (ASL < Lmin) {
						Aref = AS;
						Bref = B;
						return true;
					}
				} else {
					if (BSL < Lmin) {
						Aref = A;
						Bref = BS;
						return true;
					}
				}
				if (depth < Dmax) {
					depth++;
					A = Abak;
					B = Bbak;
					goto Next;
				}
			}
			return false;
		}
		#endregion
		#region #method# IntersectTest(Aref, Bref, Aend, Bnot, Lmin, Dmin, Dmax) 
		/// <summary>Возвращает длину и инлайны даже если они не пересекаются)</summary>
		/// <param name="Aref">Первый инлайн)</param>
		/// <param name="Bref">Второй инлайн)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="Dmin">Минимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="bound">Ограничитель разбора при заглублении)</param>
		/// <returns>Растояние между пересечениями)</returns>
		public static double IntersectTest(ref Inline Aref, ref Inline Bref, bool Aend = false, bool Bnot = false, double Lmin = 0.1, int Dmin = 10, int Dmax = 12, int bound = 20) {
			bool O;
			var A = Aref.New;
			if (Aend) A = A.NewNot;
			var B = Bref.New;
			if (Aend ^ Bnot) B = B.NewNot;
			var depth = Dmin;
			var Abak = A;
			var Bbak = B;
			Inline AB, AA, BB, BA;
			if (A.Intersect(B)) {
			Next:
				if (A.Depth < MaxDepth && B.Depth < MaxDepth) {
					AB = A.Below; AA = A.Above; BB = B.Below; BA = B.Above;
					var ABB = AB.Intersect(B, depth, bound);
					var AAB = AA.Intersect(B, depth, bound);
					if (AAB && !ABB) { A = AA; goto Next; }
					if (ABB && !AAB) { A = AB; goto Next; }
					var BBA = BB.Intersect(A, depth, bound);
					var BAA = BA.Intersect(A, depth, bound);
					if (BAA && !BBA) { B = BA; goto Next; }
					if (BBA && !BAA) { B = BB; goto Next; }

					if (ABB && AAB) { A = AB; goto Next; }
					if (BBA && BAA) { B = BB; goto Next; }
				}
				//int C;
				//do { C = IntersectEnd(ref A, ref B, depth); } while (C > 1 && A.Depth < MaxDepth && B.Depth < MaxDepth);
				var AS = A;
				do {
					O = false;
					AA = AS.OtherGreater(B);
					if (AA != null) {
						AS = AA; O = true;
					}
				} while (O);
				do {
					O = false;
					AB = AS.Above;
					AA = AS.Below;
					var abl = AB.Len(B);
					var aal = AA.Len(B);
					if (abl < aal) { AS = AB; O = true; }
					if (abl > aal) { AS = AA; O = true; }
				} while (O);
				var BS = B;
				do {
					O = false;
					BB = BS.OtherGreater(A);
					if (BB != null) {
						BS = BB; O = true;
					}
				} while (O);
				do {
					O = false;
					BB = BS.Above;
					BA = BS.Below;
					var bbl = BB.Len(A);
					var bal = BA.Len(A);
					if (bbl < bal) { BS = BB; O = true; }
					if (bbl > bal) { BS = BA; O = true; }
				} while (O);
				var ASS = A;
				var BSS = B;
				do {
					O = false;
					AA = ASS.OtherGreater(BSS);
					if (AA != null) {
						ASS = AA; O = true;
					}
					BB = BSS.OtherGreater(ASS);
					if (BB != null) {
						BSS = BB; O = true;
					}
				} while (O);
				do {
					O = false;
					AB = ASS.Above;
					AA = ASS.Below;
					BB = BSS.Above;
					BA = BSS.Below;
					var abl = AB.Len(BSS);
					var aal = AA.Len(BSS);
					var bbl = BB.Len(ASS);
					var bal = BA.Len(ASS);
					var al = abl < aal ? abl : aal;
					var bl = bbl < bal ? bbl : bal;
					if (al < bl || bbl == bal) {
						if (abl < aal) { ASS = AB; O = true; }
						if (abl > aal) { ASS = AA; O = true; }
					}
					if (al > bl || abl == aal) {
						if (bbl < bal) { BSS = BB; O = true; }
						if (bbl > bal) { BSS = BA; O = true; }
					}
				} while (O);
				var SSL = ASS.Len(BSS);
				var ASL = AS.Len(B);
				var BSL = BS.Len(A);
				if (SSL < ASL && SSL < BSL) {
					if (SSL < Lmin || depth == Dmax) {
						Aref = ASS;
						Bref = BSS;
						return SSL;
					}
				} else if (ASL < BSL) {
					if (ASL < Lmin || depth == Dmax) {
						Aref = AS;
						Bref = B;
						return ASL;
					}
				} else {
					if (BSL < Lmin || depth == Dmax) {
						Aref = A;
						Bref = BS;
						return BSL;
					}
				}
				//var L = A.Len(B);
				//if (L < Lmin || depth == Dmax) {
				//	Aref = A;
				//	Bref = B;
				//	return L;
				//}
				if (depth < Dmax) {
					depth++;
					A = Abak;
					B = Bbak;
					goto Next;
				}
			}
			return -1.0;
		}
		#endregion
		#region #method# ToString 
		public override string ToString() {
			var I = System.Globalization.CultureInfo.InvariantCulture;
			return $" Depth = {(this.Depth).ToString(I)} Root = {(this.Root).ToString("R", I)} X = {(this.X).ToString("R", I)} Y = {(this.Y).ToString("R", I)}";
		}
		#endregion
		#region #invisible# #get# New 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Inline New {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => new Inline(x0, y0, x1, y1, Not: this.Not);
		}
		#endregion
		#region #invisible# #get# NewNot 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Inline NewNot {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => new Inline(x0, y0, x1, y1, Not: !this.Not);
		}
		#endregion
	}
	#endregion
}
