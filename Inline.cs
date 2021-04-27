namespace Wholemy {
	#region #class# Inline 
	public class Inline {
		#region #class# Quadratic 
		public class Quadratic : Inline {
			public readonly double x2;
			public readonly double y2;
			private readonly double ax2;
			private readonly double ay2;
			private readonly double bx2;
			private readonly double by2;
			#region #new# (x0, y0, x1, y1, x2, y2, S = 0.5, I = 0.5, O = null) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Quadratic(double x0, double y0, double x1, double y1, double x2, double y2, double S = 0.5, double I = 0.5, Inline O = null) : base(O, S, I, x0, y0, x2, y2) {
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
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x1;
				var y22 = y1;
				var x01 = (x11 - x00) * 0.5 + x00;
				var y01 = (y11 - y00) * 0.5 + y00;
				var x12 = (x22 - x11) * 0.5 + x11;
				var y12 = (y22 - y11) * 0.5 + y11;
				this.X = (x01 - x12) * 0.5 + x12;
				this.Y = (y01 - x12) * 0.5 + x12;
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
				var x02 = (x01 - x12) * root + x12;
				var y02 = (y01 - x12) * root + x12;
				var S = root * this.Size;
				b0 = new Quadratic(x00, y00, x01, y01, x02, y02);
				b1 = new Quadratic(x02, y02, x12, y12, x22, y22);
			}
			public override void Div(double root, out Inline b0, out Inline b1, out double X, out double Y) {
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
				var x02 = (x01 - x12) * root + x12;
				var y02 = (y01 - x12) * root + x12;
				X = x02;
				Y = y02;
				var S = root * this.Size;
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
				var x02 = (x01 - x12) * root + x12;
				var y02 = (y01 - x12) * root + x12;
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
				get => new Quadratic(x0, y0, x2, y2, x1, y1);
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
						this.belowb = new Quadratic(this.x0, this.y0, this.bx2, this.by2, X, Y, S, this.Root - S, this);
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
						this.aboveb = new Quadratic(X, Y, this.ax2, this.ay2, this.x1, this.y1, this.Size - S, this.Root + S, this);
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
			public Cubic(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, double S = 0.5, double I = 0.5, Inline O = null) : base(O, S, I, x0, y0, x3, y3) {
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
				var S = root * this.Size;
				b0 = new Cubic(x00, y00, x01, y01, x02, y02, x03, y03);
				b1 = new Cubic(x03, y03, x13, y13, x23, y23, x33, y33);
			}
			public override void Div(double root, out Inline b0, out Inline b1, out double X, out double Y) {
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
				var S = root * this.Size;
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
				get => new Cubic(x0, y0, x2, y2, x3, y3, x1, y1);
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
						this.belowb = new Cubic(this.x0, this.y0, this.bx2, this.by2, this.bx3, this.by3, X, Y, S, this.Root - S, this);
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
						this.aboveb = new Cubic(X, Y, this.ax2, this.ay2, this.ax3, this.ay3, this.x1, this.y1, this.Size - S, this.Root + S, this);
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
					this.belowb = new Inline(this.x0, this.y0, this.X, this.Y, S, this.Root - S, this);
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
					this.aboveb = new Inline(this.X, this.Y, this.x1, this.y1, this.Size - S, this.Root + S, this);
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
		public bool GreaterX(Inline a, Inline b, bool e = false) {
			var aX = a.X;
			var bX = b.X;
			var tX = this.X;
			if (aX < bX) {
				if (tX >= aX) {
					if (e) {
						return (tX <= bX);
					} else {
						return (tX < bX);
					}
				}
			} else if (aX > bX) {
				if (tX <= aX) {
					if (e) {
						return (tX >= bX);
					} else {
						return (tX > bX);
					}
				}
			}
			return false;
		}
		public bool GreaterY(Inline a, Inline b, bool e = false) {
			var aY = a.Y;
			var bY = b.Y;
			var tY = this.Y;
			if (aY < bY) {
				if (tY >= aY) {
					if (e) {
						return (tY <= bY);
					} else {
						return (tY < bY);
					}
				}
			} else if (aY > bY) {
				if (tY <= aY) {
					if (e) {
						return (tY >= bY);
					} else {
						return (tY > bY);
					}
				}
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
		private Inline(Inline O, double S, double I, double x0, double y0, double x1, double y1) {
			this.Parent = O;
			this.Size = S;
			this.Root = I;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
		}
		#endregion
		#region #new# (x0, y0, x1, y1, S = 0.5, I = 0.5, O = null) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public Inline(double x0, double y0, double x1, double y1, double S = 0.5, double I = 0.5, Inline O = null) {
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
			var S = root * this.Size;
			b0 = new Inline(x00, y00, x01, y01);
			b1 = new Inline(x01, y01, x11, y11);
		}
		public virtual void Div(double root, out Inline b0, out Inline b1, out double X, out double Y) {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * root + x00;
			var y01 = (y11 - y00) * root + y00;
			X = x01;
			Y = y01;
			var S = root * this.Size;
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
			public Inline[] A;
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
			#region #method# Intersect(aa, bb, B, Alast, Blast) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public bool Intersect(ref Inline aa, ref Inline bb, Between B, bool Alast, bool Blast) {
				var A = this.A;
				var al = A.Length;
				var ba = B.A;
				var bl = ba.Length;
				if (Alast) {
					for (var ai = 0; ai < al; ai++) {
						var a = A[ai];
						if (Blast) {
							for (var bi = 0; bi < bl; bi++) {
								var b = ba[bi];
								if (a.Intersect(b)) { aa = a; bb = b; return true; }
							}
						} else {
							for (var bi = bl - 1; bi >= 0; bi--) {
								var b = ba[bi];
								if (a.Intersect(b)) { aa = a; bb = b; return true; }
							}
						}
					}
				} else {
					for (var ai = al - 1; ai >= 0; ai--) {
						var a = A[ai];
						if (Blast) {
							for (var bi = 0; bi < bl; bi++) {
								var b = ba[bi];
								if (a.Intersect(b)) { aa = a; bb = b; return true; }
							}
						} else {
							for (var bi = bl - 1; bi >= 0; bi--) {
								var b = ba[bi];
								if (a.Intersect(b)) { aa = a; bb = b; return true; }
							}
						}
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
		#region #method# Intersect(b, depth) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private bool Intersect(Inline b, int depth) {
			if (this.Intersect(b)) {
				if (depth > 0) {
					var aa = this.Bet();
					var bb = b.Bet();
					if (!aa.Intersect(bb)) return false;
					while (--depth > 0) {
						aa = aa.Bet();
						bb = bb.Bet();
						if (!aa.Intersect(bb)) return false;
					}
				}
				return true;
			}
			return false;
		}
		#endregion
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private static int IntersectBetweenFull(ref Inline Aref, ref Inline Bref, bool Alast, bool Blast, int depth = 7) {
			var A = Aref;
			var B = Bref;
			var C = 0;
			if (A.Intersect(B)) {
				if (depth > 0) {
					var aa = A.Bet();
					var bb = B.Bet();
					if (aa.Intersect(ref A, ref B, bb, Alast, Alast)) {
						Aref = A.Parent;
						Bref = B.Parent;
						C++;
					}
					while (--depth > 0) {
						aa = aa.Bet();
						bb = bb.Bet();
						if (aa.Intersect(ref A, ref B, bb, Alast, Alast)) {
							Aref = A.Parent;
							Bref = B.Parent;
							C++;
						}
					}
				}
			}
			return C;
		}
		#region #method# Intersect(Aref, Bref, Aend, Bnot, Lmin = 0.5, depth = 5) 
		/// <summary>Возвращает истину если инлайны пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый инлайн)</param>
		/// <param name="Bref">Второй инлайн)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="depth">Глубина парного перебора в битвине)</param>
		/// <returns></returns>
		public static bool Intersect(ref Inline Aref, ref Inline Bref, bool Aend = false, bool Bnot = false, double Lmin = 0.1, int depth = 7) {
			var A = Aref;
			var B = Bref;
			var coutA = 0;
			var coutB = 0;
			Bnot = Aend ^ Bnot;
			if (A.Intersect(B)) {
			Next:
				if (coutA < 70 && coutB < 70) {
					Inline AB, AA, BB, BA;
					if (Aend) { AB = A.Above; AA = A.Below; } else { AB = A.Below; AA = A.Above; }
					if (Bnot) { BB = B.Above; BA = B.Below; } else { BB = B.Below; BA = B.Above; }
					var ABB = AB.Intersect(B, depth);
					var AAB = AA.Intersect(B, depth);
					var BBA = BB.Intersect(A, depth);
					var BAA = BA.Intersect(A, depth);
					if (ABB && !AAB) { A = AB; coutA++; goto Next; }
					if (BBA && !BAA) { B = BB; coutB++; goto Next; }
					if (AAB && !ABB) { A = AA; coutA++; goto Next; }
					if (BAA && !BBA) { B = BA; coutB++; goto Next; }
					if (ABB && AAB && BBA && BAA) { A = AB; coutA++; B = BB; coutB++; goto Next; }
				}
				//int C;
				//do { C = IntersectBetweenFull(ref A, ref B, Aend, Bnot, depth); } while (C > 1 && A.Depth < 70 && B.Depth < 70);
				bool O;
				do {
					O = false;
					var AA = A.OtherGreater(B);
					if (AA != null) {
						A = AA; O = true;
					}
					var BB = B.OtherGreater(A);
					if (BB != null) {
						B = BB; O = true;
					}
				} while (O);
				O = true;
				while (O) {
					O = false;
					var AB = A.Above;
					var AA = A.Below;
					var abl = AB.Len(B);
					var aal = AA.Len(B);
					var BB = B.Above;
					var BA = B.Below;
					var bbl = BB.Len(A);
					var bal = BA.Len(A);
					var al = abl < aal ? abl : aal;
					var bl = bbl < bal ? bbl : bal;
					if (al < bl || bbl == bal) {
						if (abl < aal) { A = AB; O = true; }
						if (abl > aal) { A = AA; O = true; }
					}
					if (al >= bl || abl == aal) {
						if (bbl < bal) { B = BB; O = true; }
						if (bbl > bal) { B = BA; O = true; }
					}
				}

				if ((A.X == B.X && A.Y == B.Y) || (A.Len(B) <= Lmin)) {
					Aref = A;
					Bref = B;
					return true;
				}
			}
			return false;
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
			get => new Inline(x0, y0, x1, y1);
		}
		#endregion
	}
	#endregion
}
