namespace Wholemy {
	#region #class# Bezier 
	public class Bezier {
		public const int MaxDepth = 64;
		public const int PresetExistsMax = 2;
		public const int PresetDepthMin = 5;
		public const int PresetDepthMax = 10;
		public const double PresetLengthMin = 0.25;
		#region #class# Quadratic 
		public class Quadratic:Bezier {
			public readonly double x2;
			public readonly double y2;
			private readonly double ax2;
			private readonly double ay2;
			private readonly double bx2;
			private readonly double by2;
			#region #new# (x0, y0, x1, y1, x2, y2, Root = 0.5, Size = 0.5, O = null) 
			public Quadratic(double x0,double y0,double x1,double y1,double x2,double y2,double Root = 0.5,double Size = 0.5,Bezier O = null,bool Not = false) : base(O,Root,Size,x0,y0,x2,y2,Not) {
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
			public override void Div(double root,out Bezier b0,out Bezier b1) {
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
				var S = this.Size * 0.5;
				var A = new Quadratic(x00,y00,x01,y01,x02,y02,this.Root - S,S,this,this.Not);
				var B = new Quadratic(x02,y02,x12,y12,x22,y22,this.Root + S,S,this,this.Not);
				if (this.Not) {
					this.aboveb = b1 = B;
					this.belowb = b0 = A;
				} else {
					this.belowb = b0 = A;
					this.aboveb = b1 = B;
				}
			}
			#endregion
			#region #method# Div(root, X, Y, b0, b1) 
			public override void Div(double root,double X,double Y,out Bezier b0,out Bezier b1) {
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
				if (System.Math.Round(x02,1) != System.Math.Round(X,1) || System.Math.Round(y02,1) != System.Math.Round(Y,1))
					throw new System.InvalidOperationException();
				var S = this.Size * 0.5;
				var A = new Quadratic(x00,y00,x01,y01,X,Y,this.Root - S,S,this,this.Not);
				var B = new Quadratic(X,Y,x12,y12,x22,y22,this.Root + S,S,this,this.Not);
				if (this.Not) {
					this.aboveb = b1 = B;
					this.belowb = b0 = A;
				} else {
					this.belowb = b0 = A;
					this.aboveb = b1 = B;
				}
				//this.belowb = b0 = A;
				//this.aboveb = b1 = B;
			}
			#endregion
			#region #override# #method# Get(root, X, Y) 
			public override void Get(double root,out double X,out double Y) {
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
			#region #invisible# #get# Pastle 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Pastle {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(x0,y0,x2,y2,x1,y1,Not: this.Not);
			}
			#endregion
			#region #invisible# #get# Return 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Return {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Quadratic(x0,y0,x2,y2,x1,y1,Not: !this.Not);
			}
			#endregion
			#region #invisible# #get# Invert 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Invert {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Quadratic(x1,y1,x2,y2,x0,y0,Not: !this.Not);
				}
			}
			#endregion
			#region #invisible# #get# Incest 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Incest {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Quadratic(x1,y1,x2,y2,x0,y0,Not: this.Not);
				}
			}
			#endregion
			#region #get# Below 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Below {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.belowb == null) {
						var S = this.Size * 0.5;
						if (this.Not) {
							this.belowb = new Quadratic(X,Y,this.ax2,this.ay2,this.x1,this.y1,this.Root + S,S,this,this.Not);
						} else {
							this.belowb = new Quadratic(this.x0,this.y0,this.bx2,this.by2,X,Y,this.Root - S,S,this,this.Not);
						}
					}
					return this.belowb;
				}
			}
			#endregion
			#region #get# Above 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Above {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.aboveb == null) {
						var S = this.Size * 0.5;
						if (this.Not) {
							this.aboveb = new Quadratic(this.x0,this.y0,this.bx2,this.by2,X,Y,this.Root - S,S,this,this.Not);
						} else {
							this.aboveb = new Quadratic(X,Y,this.ax2,this.ay2,this.x1,this.y1,this.Root + S,S,this,this.Not);
						}
					}
					return this.aboveb;
				}
			}
			#endregion
			#region #property# Line 
			public override Wins.PathSource.Line Line {
				get { return new Wins.PathSource.Line2(x0,y0,x2,y2,x1,y1); }
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"Q x0={(this.x0).ToString("R",I)} y0={(this.y0).ToString("R",I)} x2={(this.x2).ToString("R",I)} y2={(this.y2).ToString("R",I)} x1={(this.x1).ToString("R",I)} y1={(this.y1).ToString("R",I)}";
			}
			#endregion
			#region #property# Length 
			public override double Length {
				get {
					var x = System.Math.Abs(x0 - X) + System.Math.Abs(x1 - X);
					var y = System.Math.Abs(y0 - Y) + System.Math.Abs(y1 - Y);
					return System.Math.Sqrt(x * x + y * y);
				}
			}
			#endregion
			#region #method# Equ(B) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override bool Equ(Bezier b) {
				var B = b as Quadratic;
				if (B == null) return false;
				return (this.x0 == B.x0 && this.y0 == B.y0) && (this.x1 == B.x1 && this.y1 == B.y1) && (this.x2 == B.x2 && this.y2 == B.y2);
			}
			#endregion
		}
		#endregion
		#region #class# Cubic 
		public class Cubic:Bezier {
			private static readonly double Arc = 4.0 / 3.0 * System.Math.Tan(System.Math.PI * 0.125);
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
			#region #new# (x0, y0, x1, y1, cw, Not) 
			public Cubic(double x0,double y0,double x1,double y1,bool cw = false,bool Not = false) : base(null,0.5,0.5,x0,y0,x1,y1,Not) {
				var x2 = x0;
				var y2 = y0;
				var x3 = x1;
				var y3 = y1;
				if (((x0 < x1 && y0 < y1) || (x0 > x1 && y0 > y1)) ^ cw) {
					x2 += ((x1 - x0) * Arc);
					y3 += ((y0 - y1) * Arc);
				} else {
					y2 += ((y1 - y0) * Arc);
					x3 += ((x0 - x1) * Arc);
				}
				this.x2 = x2;
				this.y2 = y2;
				this.x3 = x3;
				this.y3 = y3;
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
			#region #new# (x0, y0, x1, y1, x2, y2, x3, y3, Root = 0.5, Size = 0.5, O = null) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Cubic(double x0,double y0,double x1,double y1,double x2,double y2,double x3,double y3,double Root = 0.5,double Size = 0.5,Bezier O = null,bool Not = false) : base(O,Root,Size,x0,y0,x3,y3,Not) {
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
			public override void Div(double root,out Bezier b0,out Bezier b1) {
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
				var S = this.Size * 0.5;
				var A = new Cubic(x00,y00,x01,y01,x02,y02,x03,y03,this.Root - S,S,this,this.Not);
				var B = new Cubic(x03,y03,x13,y13,x23,y23,x33,y33,this.Root + S,S,this,this.Not);
				if (this.Not) {
					this.aboveb = b1 = B;
					this.belowb = b0 = A;
				} else {
					this.belowb = b0 = A;
					this.aboveb = b1 = B;
				}
			}
			#endregion
			#region #method# Div(root, X, Y, b0, b1) 
			public override void Div(double root,double X,double Y,out Bezier b0,out Bezier b1) {
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
				//if (System.Math.Round(x03,1) != System.Math.Round(X,1) || System.Math.Round(y03,1) != System.Math.Round(Y,1))
				//	throw new System.InvalidOperationException();
				var S = this.Size * 0.5;
				var A = new Cubic(x00,y00,x01,y01,x02,y02,X,Y,this.Root - S,S,this,this.Not);
				var B = new Cubic(X,Y,x13,y13,x23,y23,x33,y33,this.Root + S,S,this,this.Not);
				if (this.Not) {
					this.aboveb = b1 = B;
					this.belowb = b0 = A;
				} else {
					this.belowb = b0 = A;
					this.aboveb = b1 = B;
				}
				//this.belowb = b0 = A;
				//this.aboveb = b1 = B;
			}
			#endregion
			#region #override# #method# Get(root, X, Y) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override void Get(double root,out double X,out double Y) {
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
			#region #invisible# #get# Pastle 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Pastle {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(x0,y0,x2,y2,x3,y3,x1,y1,Not: this.Not);
			}
			#endregion
			#region #invisible# #get# Return 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Return {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get => new Cubic(x0,y0,x2,y2,x3,y3,x1,y1,Not: !this.Not);
			}
			#endregion
			#region #invisible# #get# Invert 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Invert {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Cubic(x1,y1,x3,y3,x2,y2,x0,y0,Not: !this.Not);
				}
			}
			#endregion
			#region #invisible# #get# Incest 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Incest {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					return new Cubic(x1,y1,x3,y3,x2,y2,x0,y0,Not: this.Not);
				}
			}
			#endregion
			#region #get# Below 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Below {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.belowb == null) {
						var S = this.Size * 0.5;
						if (this.Not) {
							this.belowb = new Cubic(X,Y,this.ax2,this.ay2,this.ax3,this.ay3,this.x1,this.y1,this.Root + S,S,this,this.Not);
						} else {
							this.belowb = new Cubic(this.x0,this.y0,this.bx2,this.by2,this.bx3,this.by3,X,Y,this.Root - S,S,this,this.Not);
						}
					}
					return this.belowb;
				}
			}
			#endregion
			#region #get# Above 
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public override Bezier Above {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if (this.aboveb == null) {
						var S = this.Size * 0.5;
						if (this.Not) {
							this.aboveb = new Cubic(this.x0,this.y0,this.bx2,this.by2,this.bx3,this.by3,X,Y,this.Root - S,S,this,this.Not);
						} else {
							this.aboveb = new Cubic(X,Y,this.ax2,this.ay2,this.ax3,this.ay3,this.x1,this.y1,this.Root + S,S,this,this.Not);
						}
					}
					return this.aboveb;
				}
			}
			#endregion
			#region #property# Line 
			public override Wins.PathSource.Line Line {
				get { return new Wins.PathSource.Line3(x0,y0,x2,y2,x3,y3,x1,y1); }
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"C x0={(this.x0).ToString("R",I)} y0={(this.y0).ToString("R",I)} x2={(this.x2).ToString("R",I)} y2={(this.y2).ToString("R",I)} x3={(this.x3).ToString("R",I)} y3={(this.y3).ToString("R",I)} x1={(this.x1).ToString("R",I)} y1={(this.y1).ToString("R",I)}";
			}
			#endregion
			#region #property# Length 
			public override double Length {
				get {
					var x = System.Math.Abs(x0 - X) + System.Math.Abs(x1 - X);
					var y = System.Math.Abs(y0 - Y) + System.Math.Abs(y1 - Y);
					return System.Math.Sqrt(x * x + y * y);
				}
			}
			#endregion
			#region #method# Equ(B) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public override bool Equ(Bezier b) {
				var B = b as Cubic;
				if (B == null) return false;
				return (this.x0 == B.x0 && this.y0 == B.y0) && (this.x1 == B.x1 && this.y1 == B.y1) && (this.x2 == B.x2 && this.y2 == B.y2) && (this.x3 == B.x3 && this.y3 == B.y3);
			}
			#endregion
		}
		#endregion
		public readonly double Root;
		public readonly double Size;
		private double L;
		private double T;
		private double R;
		private double B;
		public readonly double x0;
		public readonly double y0;
		public readonly double x1;
		public readonly double y1;
		public double X;
		public double Y;
		public readonly int Depth;
		private readonly Bezier Parent;
		public readonly bool Not;
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
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Bezier DepthAbove {
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
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Bezier DepthBelow {
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
		#region #get# Below 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		private Bezier belowb;
		public virtual Bezier Below {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.belowb == null) {
					var S = this.Size * 0.5;
					if (this.Not) {
						this.belowb = new Bezier(this.X,this.Y,this.x1,this.y1,this.Root + S,S,this,this.Not);
					} else {
						this.belowb = new Bezier(this.x0,this.y0,this.X,this.Y,this.Root - S,S,this,this.Not);
					}
				}
				return this.belowb;
			}
		}
		#endregion
		#region #get# Above 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		private Bezier aboveb;
		public virtual Bezier Above {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.aboveb == null) {
					var S = this.Size * 0.5;
					if (this.Not) {
						this.aboveb = new Bezier(this.x0,this.y0,this.X,this.Y,this.Root - S,S,this,this.Not);
					} else {
						this.aboveb = new Bezier(this.X,this.Y,this.x1,this.y1,this.Root + S,S,this,this.Not);
					}
				}
				return this.aboveb;
			}
		}
		#endregion
		#region #property# OtherAbove 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Bezier OtherAbove {
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
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public Bezier OtherBelow {
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
		public Bezier MaxBelow {
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
		public Bezier MaxAbove {
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
		public Bezier OtherGreater(Bezier B) {
			var OA = this.OtherAbove;
			if (OA != null && OA.Greater(this,B)) return OA;
			var OB = this.OtherBelow;
			if (OB != null && OB.Greater(this,B)) return OB;
			return null;
		}
		#endregion
		#region #method# Greater(a, b) 
		/// <summary>Возвращает истину, если X и Y этой линии больше чем A и меньше или равно B)</summary>
		public bool Greater(Bezier a,Bezier b,bool equ = false) {
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
		private Bezier(Bezier O,double RangeBelow,double RangeAbove,double x0,double y0,double x1,double y1,bool Not) {
			this.Parent = O;
			this.Root = RangeBelow;
			this.Size = RangeAbove;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
			this.Not = Not;
		}
		#endregion
		#region #new# (x0, y0, x1, y1, Root = 0.5, Size = 0.5, O = null) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public Bezier(double x0,double y0,double x1,double y1,double Root = 0.5,double Size = 0.5,Bezier O = null,bool Not = false) {
			this.Parent = O;
			this.Root = Root;
			this.Size = Size;
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
		public virtual void Div(double root,out Bezier b0,out Bezier b1) {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * root + x00;
			var y01 = (y11 - y00) * root + y00;
			var S = this.Size * 0.5;
			var A = new Bezier(x00,y00,x01,y01,this.Root - S,S,this,this.Not);
			var B = new Bezier(x01,y01,x11,y11,this.Root + S,S,this,this.Not);
			if (this.Not) {
				this.aboveb = b1 = B;
				this.belowb = b0 = A;
			} else {
				this.belowb = b0 = A;
				this.aboveb = b1 = B;
			}
		}
		#endregion
		#region #virtual# #method# Div(root, X, Y, b0, b1) 
		public virtual void Div(double root,double X,double Y,out Bezier b0,out Bezier b1) {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * root + x00;
			var y01 = (y11 - y00) * root + y00;
			//if (System.Math.Round(x01,1) != System.Math.Round(X,1)
			//|| System.Math.Round(y01,1) != System.Math.Round(Y,1))
			//	throw new System.InvalidOperationException();
			var S = this.Size * 0.5;
			var A = new Bezier(x00,y00,X,Y,this.Root - S,S,this,this.Not);
			var B = new Bezier(X,Y,x11,y11,this.Root + S,S,this,this.Not);
			if (this.Not) {
				this.aboveb = b1 = B;
				this.belowb = b0 = A;
			} else {
				this.belowb = b0 = A;
				this.aboveb = b1 = B;
			}
			//this.belowb = b0 = A;
			//this.aboveb = b1 = B;
		}
		#endregion
		#region #virtual# #method# Get(root, X, Y) 
		public virtual void Get(double root,out double X,out double Y) {
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
		#region #property# Length 
		public virtual double Length {
			get {
				var x = x0 - x1;
				var y = y0 - y1;
				return System.Math.Sqrt(x * x + y * y);
			}
		}
		#endregion
		#region #method# Len(x0, y0, x1, y1) 
		private static double Len(double x0,double y0,double x1,double y1) {
			var x = x0 - x1;
			var y = y0 - y1;
			return System.Math.Sqrt(x * x + y * y);
		}
		#endregion
		#region #method# Geron(x0, y0, x1, y1, x2, y2) 
		/// <summary>Возвращает площадь треугольника)</summary>
		/// <param name="x0">Первая координата X)</param>
		/// <param name="y0">Первая координата Y)</param>
		/// <param name="x1">Вторая координата X)</param>
		/// <param name="y1">Вторая координата Y)</param>
		/// <param name="x2">Третья координата X)</param>
		/// <param name="y2">Третья координата Y)</param>
		/// <returns>Площадь)</returns>
		private static double Geron(double x0,double y0,double x1,double y1,double x2,double y2) {
			var a = Len(x0,y0,x1,y1);
			var b = Len(x1,y1,x2,y2);
			var c = Len(x2,y2,x0,y0);
			var p = 0.5 * (a + b + c);
			return System.Math.Sqrt(p * (p - a) * (p - b) * (p - c));
		}
		#endregion
		#region #method# Len(a) 
		private double Len(Bezier a) {
			var x1 = this.X - a.X;
			var y1 = this.Y - a.Y;
			return System.Math.Sqrt(x1 * x1 + y1 * y1);
		}
		#endregion
		#region #method# Len(x, y) 
		private static double Len(double x,double y) {
			return System.Math.Sqrt(x * x + y * y);
		}
		#endregion
		#region #method# Intersect(a) 
		private bool Intersect(Bezier a) {
			return (a.R >= this.L && this.R >= a.L && a.B >= this.T && this.B >= a.T);
		}
		#endregion
		#region #class# Chance 
		private class Chance {
			public readonly int Cout;
			public readonly Bezier Line;
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
			private Chance(Chance Next,Bezier Line) {
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
			public static Chance operator +(Chance Next,Bezier Line) {
				return new Chance(Next,Line);
			}
			#endregion
			#region #implicit operator# (Line)
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public static implicit operator Chance(Bezier Line) {
				return new Chance(null,Line);
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
				Bezier L;
				var C = this.Prev;
				var S = this;
				int Min = 0, Max = 0;
				if (bound >= 2) {
					while (S != null && !S.ExistsAbove && !S.ExistsBelow) { S = S.Next; }
					if (S == null) S = this;
					Max = S.Cout - bound;
					while (C != S && !C.ExistsAbove && !C.ExistsBelow) { C = C.Prev; }
					Min = C.Cout + bound;
				}
				while (C != S) {
					if (bound < 2 || C.Cout <= Min || C.Cout >= Max) {
						L = C.Line;
						if (C.ExistsAbove) { R += L.Above; }
						if (C.ExistsBelow) { R += L.Below; }
					}
					C = C.Prev;
				}
				L = S.Line;
				if (S.ExistsAbove) { R += L.Above; }
				if (S.ExistsBelow) { R += L.Below; }
				return R;
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				return $"Cout = {Cout}";
			}
			#endregion
		}
		#endregion
		#region #class# Figure 
		public class Figure {
			#region #class# Base 
			public class Base {
				public int Count;
				#region #new# (Start) 
				public Base(Figure Start) {
					var Line = Start.Line;
					this.Count = 1;
					this.L = Line.L;
					this.T = Line.T;
					this.R = Line.R;
					this.B = Line.B;
				}
				#endregion
				public double L; public double T; public double R; public double B;
			}
			#endregion
			public Base Over;
			public Bezier Line;
			public Figure Next;
			public Figure Prev;
			public Figure Next10;
			public Figure Next01;
			public Figure AltNext;
			public Figure PreNext;
			public Figure AltPrev;
			public Figure AltOver;
			public bool AltStop;
			private int AltTyped;
			public int AltIndex;
			/// <summary>Тип фигуры устанавливаемый комбайном)</summary>
			public int Type;
			#region #new# (Figure, Inline) 
			public Figure(Figure Figure,Bezier Inline,bool Invert = false) {
				this.Line = Inline;
				if (Figure != null) {
					var Over = this.Over = Figure.Over; Over.Count++;
					if (Over.L > Inline.L) Over.L = Inline.L;
					if (Over.T > Inline.T) Over.T = Inline.T;
					if (Over.R < Inline.R) Over.R = Inline.R;
					if (Over.B < Inline.B) Over.B = Inline.B;
				} else {
					this.Over = new Base(this);
				}
				if (Figure != null) {

					if (Figure.Line.Not != Inline.Not) {
						throw new System.InvalidOperationException();
						//Inline = Inline.Return;
					}

					//Invert ^= Inline.Not;
					if (Invert) {
						this.Prev = Figure;
						this.Next = Figure.Next;
						this.Next.Prev = this;
						Figure.Next = this;
						var T = this.Next;
					} else {
						this.Next = Figure;
						this.Prev = Figure.Prev;
						this.Prev.Next = this;
						Figure.Prev = this;
						var T = this.Prev;
					}
				} else {
					this.Prev = this;
					this.Next = this;
				}
			}
			#endregion
			#region #get# Items 
			public Bezier[] Items {
				get {
					var C = 1;
					var Root = this;
					var Last = this;
					var Item = this.Next;
					while (Item != null) { Last = Item; Item = Item.Next; C++; if (Item == this) { break; } }
					if (Item == null) {
						Item = this.Prev;
						while (Item != null) { Root = Item; Item = Item.Prev; C++; }
					}
					var A = new Bezier[C];
					var i = 0;
					if (this.Line.Not) {
						Item = Last;
						A[i++] = Item.Line;
						Item = Item.Prev;
						while (Item != null && Item != Root) {
							A[i++] = Item.Line;
							Item = Item.Prev;
						}
					} else {
						Item = Root;
						A[i++] = Item.Line;
						Item = Item.Next;
						while (Item != null && Item != Root) {
							A[i++] = Item.Line;
							Item = Item.Next;
						}
					}
					return A;
				}
			}
			#endregion
			#region #property# AboveArea 
			public Figure AboveArea {
				get {
					var T = this;
					var tt = this.AltOver;
					while (tt != null) {
						if (tt.Area > T.Area) { T = tt; }
						tt = tt.AltOver;
					}
					return T;
				}
			}
			#endregion
			#region #get# Count 
			public int Count { get { return Over.Count; } }
			#endregion
			#region #property# Length 
			public double Length {
				get {
					var L = this.Line.Length;
					var T = this.Next;
					while (T != this) {
						L += T.Line.Length;
						T = T.Next;
					}
					return L;
				}
			}
			#endregion
			#region #property# Root 
			public Figure Root {
				get {
					var Root = this;
					var T = this.Prev;
					while (T != null && T != this) {
						Root = T;
						T = T.Prev;
					}
					if (T == this) return T;
					return Root;
				}
			}
			#endregion
			#region #property# Last 
			public Figure Last {
				get {
					var Last = this;
					var T = this.Next;
					while (T != null && T != this) {
						Last = T;
						T = T.Next;
					}
					return Last;
				}
			}
			#endregion
			#region #get# Area 
			//public double AreaP {
			//	get {
			//		Bezier PL, NL;
			//		var A = 0.0;
			//		var P = this;
			//		var N = this.Next;
			//		if (N != this) {
			//		Next:
			//			PL = P.Line;
			//			NL = N.Line;
			//			A += Geron(PL.x0,PL.y0,NL.x0,NL.y0,NL.x1,NL.y1);
			//			P = N.Next;
			//			if (P == this) return A;
			//			if (P.Next != this) {
			//				N = P.Next;
			//				goto Next;
			//			}
			//		}
			//		PL = P.Line;
			//		NL = N.Line;
			//		A += Geron(NL.x0,NL.y0,PL.x0,PL.y0,PL.x1,PL.y1);
			//		return A;
			//	}
			//}
			/// <summary>Возвращает площадь пути)</summary>
			public double Area {
				get {
					var A = 0.0;
					var P = this;
					do {
						if (P == null) return 0.0;
						var p = P.Line;
						A += (p.x0 + p.X) * (p.y0 - p.Y);
						A += (p.X + p.x1) * (p.Y - p.y1);
						P = P.Next;
					}while (P != this);
					return System.Math.Abs(A / 2);
				}
			}
			#endregion
			#region #method# Intersect(a) 
			public bool Intersect(Figure a) {
				if (a == null) return false;
				var A = this.Over;
				var B = a.Over;
				if (A == B) throw new System.InvalidOperationException();
				return (A.R >= B.L && B.R >= A.L && A.B >= B.T && B.B >= A.T);
			}
			#endregion
			#region #method# ToAlt(B, Alt) 
			public void ToAltA(Figure B,ref Path Alt) {
				if (this.AltNext != null) throw new System.InvalidOperationException(); //return;
				this.AltNext = B;
				Alt = new Path(this,Alt);
			}
			#endregion
			#region #method# SetType(T) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public void SetType(int T) {
				var I = this; do { I.Type = T; I.AltNext = null; I.AltPrev = null; I.AltOver = null; I.AltIndex = 0; I = I.Next; } while (I != this);
			}
			#endregion
			#region #method# Neq(B) 
			public bool Neq(Figure B) {
				var AC = this;
				var BC = B;
				var ACC = AC;
				var BCC = BC;
				do {
					do {
						if (!AC.Line.Neq(BC.Line)) return false;
						BC = BC.Next;
					} while (BC != BCC);
					AC = AC.Next;
				} while (AC != ACC);
				return true;
			}
			#endregion
			#region #method# Seq(B) 
			public int Seq(Figure B) {
				var AC = this;
				var BC = B;
				var ACC = AC;
				var BCC = BC;
				var C = 0;
				do {
					do {
						if (AC.Line.Seq(BC.Line)) C++;
						BC = BC.Next;
					} while (BC != BCC);
					AC = AC.Next;
				} while (AC != ACC);
				return C;
			}
			#endregion
			#region #method# Meq(B) 
			public bool Meq(Figure B) {
				var AC = this;
				var BC = B;
				var ACC = AC;
				var BCC = BC;
				do {
					do {
						if (AC.Line.Meq(BC.Line)) return true;
						BC = BC.Next;
					} while (BC != BCC);
					AC = AC.Next;
				} while (AC != ACC);
				return false;
			}
			#endregion
			#region #method# Typed(I) 
			public bool Typed(int I) {
				bool R = false;
				if (this.AltTyped == 0) R = true;
				this.AltTyped = I;
				return R;
			}
			#endregion
			#region #get# Invert 
			public Figure Invert {
				get {
					Figure I = this;
					Figure F = null;
					do {
						I = I.Next;
						F = new Figure(F,I.Line.Return);
					} while (I != this);
					return F;
				}
			}
			#endregion
			#region #property# Pastle 
			public Figure Pastle {
				get {
					Figure I = this;
					Figure F = null;
					do {
						I = I.Prev;
						F = new Figure(F,I.Line.Pastle);
					} while (I != this);
					return F;
				}
			}
			#endregion
			#region #property# Mixed 
			public bool Mixed {
				get {
					var N = this.Line.Not;
					var I = this.Next;
					while (I != this) {
						if (I.Line.Not != N) return true;
						I = I.Next;
					}
					return false;
				}
			}

			#endregion
			internal Path Container(Path path,out bool cont) {
				while (path != null) {
					var acc = this;
					var bcc = path.Figure;
					var scc = bcc;
					while (bcc.Line != acc.Line) {
						bcc = bcc.Next;
						if (bcc == scc) break;
					}
					if (bcc.Line == acc.Line) {
						var tcc = bcc;
						var rcc = acc;
						while (rcc.Prev != acc && tcc.Prev.Line == rcc.Prev.Line) {
							tcc = tcc.Prev;
							rcc = rcc.Prev;
						}
						bcc = tcc;
						rcc = acc;
						while (rcc.Next != acc && tcc.Next.Line == rcc.Next.Line) {
							tcc = tcc.Next;
							rcc = rcc.Next;
						}
						if (rcc.Next == acc && tcc.Next != bcc) {
							cont = true;
							return path;
						} else {
							cont = false;
							return null;
						}
					} else {
						path = path.Next;
					}
				}
				cont = true;
				return null;
			}
		}
		#endregion
		#region #class# Path 
		public class Path {
			public Path After;
			public Path Next;
			public Path Prev;
			public Figure Figure;
			#region #new# (Figure, Next) 
			public Path(Figure Figure,Path Next = null,bool NextIsAfter = false) {
				this.Figure = Figure;
				if (NextIsAfter) {
					this.After = Next;
					this.Prev = this;
				} else {
					if (Next != null) { this.Prev = Next.Prev; Next.Prev = this; } else { this.Prev = this; }
				}
				this.Next = Next;
			}
			#endregion
			#region #method# Combine(Figure) 
			internal Path Combine(Figure Figure) {
				var P = this;
				Path R = this;
				var Co = 0;
				do {
					var Rfirst = true;
					var AC = P.Figure.Pastle;
					var BC = Figure.Pastle;
					if (AC != null && BC != null) {
						if (AC.Intersect(BC)) {
							var C = Bezier.Combine(AC,BC);
							if (C != null) {
								if (AC.Line.Not == BC.Line.Not) {
									var G = C.AboveArea.Figure.Pastle;
									P.Figure = G;
									while (C != null) {
										if (C.Figure != G && C.Figure.Neq(G))
											R = new Path(C.Figure.Invert,R);
										C = C.Next;
									}
								} else {
									if (AC.Line.Not) {
										while (C != null) {
											var S = C.Figure.Seq(BC);
											if (S == 1) {
												if (Rfirst) {
													P.Figure = C.Figure.Invert;
													Rfirst = false;
												} else {
													R = new Path(C.Figure.Invert,R);
												}
											}
											C = C.Next;
										}
									} else {
										while (C != null) {
											if (C.Figure.Seq(AC) == 1) {
												if (Rfirst) {
													P.Figure = C.Figure.Invert;
													Rfirst = false;
												} else {
													R = new Path(C.Figure.Invert,R);
												}
											}
											C = C.Next;
										}
									}
								}
								Co++;
							}
						}
					}
					P = P.Next;
				} while (P != null);
				//if(Co==0) return new Path(Figure, R);
				return R;
			}
			#endregion
			internal Path Combine(Path Path) {

				return this;
			}
			public Path Cut() {
				var Last = this;
				while (Last.Next != null) { Last = Last.Next; }
				var Root = this;
				while (Root.Prev != Last) { Root = Root.Prev; }
				if (this == Root) {
					if (this == Last) {
						return null;
					} else {
						var Next = this.Next;
						Next.Prev = Last;
						return Next;
					}
				} else {
					if (this == Last) {
						this.Prev.Next = null;
						Root.Prev = this.Prev;
						return Root;
					} else {
						var Next = this.Next;
						var Prev = this.Prev;
						Next.Prev = Prev;
						Prev.Next = Next;
						return Root;
					}
				}
			}
			public Path AboveArea {
				get {
					var T = this;
					var tt = this.Next;
					while (tt != null) {
						if (tt.Figure.Length > T.Figure.Length) { T = tt; }
						tt = tt.Next;
					}
					return T;
				}
			}
		}
		#endregion
		#region #method# Contain(A, X, Y) 
		/// <summary>Возвращает истину если указанные координаты расположены внутри указанной фигуры)</summary>
		/// <param name="A">Фигура в которой производится поиск координат)</param>
		/// <param name="X">Горизонтальная координата)</param>
		/// <param name="Y">Вертикальная координата)</param>
		/// <returns>Возвращает истину если указанные координаты расположены внутри указанной фигуры)</returns>
		public static bool Contain(Figure A,double X,double Y) {
			var AC = A;
			var BC = new Figure(null,new Bezier(A.Over.L - 10.0,A.Over.T - 10.0,X,Y));
			var acCount = AC.Count;
			var bcCount = BC.Count;
			var acNot = AC.Line.Not;
			if (acNot) AC = AC.Invert; else AC = AC.Pastle;
			var Count = 0;
			for (var ac = 0;ac < acCount;ac++) {
				for (var bc = 0;bc < bcCount;bc++) {
					double AR = 0.0, AX = 0.0, AY = 0.0, BR = 0.0, BX = 0.0, BY = 0.0;
					if (AC.Line.Neq(BC.Line) && Bezier.Intersect(AC.Line,ref AR,ref AX,ref AY,BC.Line,ref BR,ref BX,ref BY)) {
						if (BR > 0.0 && BR < 1.0) {
							BC.Line.Div(BR,BX,BY,out var bi0,out var bi1);
							BC.Line = bi1; BC = new Figure(BC,bi0);
							bcCount++;
						}
						if (AR > 0.0 && AR < 1.0) {
							AC.Line.Div(AR,AX,AY,out var ai0,out var ai1);
							AC.Line = ai1; AC = new Figure(AC,ai0);
							acCount++;
						}
						Count++;
					}
					BC = BC.Next;
				}
				AC = AC.Next;
			}
			return Count % 2 == 0;
		}
		#endregion
		#region #method# Combine(A, B) 
		public static Path Combine(Figure A,Figure B) {
			var AC = A;
			var BC = B;
			Path path = null;
			Figure acc = null, bcc = null, ccc = null;
			Figure AB = null, BA = null;
			var acCount = AC.Count;
			var bcCount = BC.Count;
			var acNot = AC.Line.Not;
			var bcNot = BC.Line.Not;
			for (var ac = 0;ac < acCount;ac++) {
				for (var bc = 0;bc < bcCount;bc++) {
					double AR = 0.0, AX = 0.0, AY = 0.0, BR = 0.0, BX = 0.0, BY = 0.0;
					if (AC.Line.Neq(BC.Line) && Bezier.PIntersect(AC.Line,ref AR,ref AX,ref AY,BC.Line,ref BR,ref BX,ref BY)) {
						if (BR > 0.0 && BR < 1.0) {
							BC.Line.Div(BR,BX,BY,out var bi0,out var bi1);
							BC.Line = bi1; BC = new Figure(BC,bi0,bcNot);
							bcCount++;
						}
						if (AR > 0.0 && AR < 1.0) {
							AC.Line.Div(AR,AX,AY,out var ai0,out var ai1);
							AC.Line = ai1; AC = new Figure(AC,ai0,acNot);
							acCount++;
						}
					}
					if (bcNot) BC = BC.Prev; else BC = BC.Next;
				}
				if (acNot) AC = AC.Prev; else AC = AC.Next;
			}
			if (bcNot) BC = BC.Invert; else BC = BC.Pastle;
			if (acNot) AC = AC.Invert; else AC = AC.Pastle;
			var ACC = AC;
			var BCC = BC;
			AC.SetType(1);
			BC.SetType(2);
			Path rtA = null;
			Path rtB = null;
			do {
				do {
					if (Equ(AC.Next.Line,AC.Line,BC.Line,BC.Prev.Line)) {
						if (AC.AltNext != null) throw new System.InvalidOperationException();
						AC.AltNext = BC;
						rtA = new Path(AC,rtA);
					}
					if (Equ(BC.Next.Line,BC.Line,AC.Line,AC.Prev.Line)) {
						if (BC.AltNext != null) throw new System.InvalidOperationException();
						BC.AltNext = AC;
						rtB = new Path(BC,rtB);
					}
					BC = BC.Next;
				} while (BC != BCC);
				AC = AC.Next;
			} while (AC != ACC);
			var trA = rtA;
			while (trA != null) {
				var F = trA.Figure;
				F.Next = F.AltNext;
				F.AltNext.Prev = F;
				trA = trA.Next;
			}
			var trB = rtB;
			while (trB != null) {
				var F = trB.Figure;
				F.Next = F.AltNext;
				F.AltNext.Prev = F;
				trB = trB.Next;
			}
			return rtA;
		}
		#endregion
		#region #method# Intersect(b, depth) 
		private bool Intersect(Bezier b,int depth,int bound) {
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
		}
		#endregion
		#region #method# IntersectFor(Aref, Bref, depth, bound) 
		private static void IntersectFor(ref Bezier Aref,ref Bezier Bref,int depth,int bound) {
			Bezier AB, AA, BB, BA;
			var A = Aref;
			var B = Bref;
		Next:
			if (A.Depth < MaxDepth && B.Depth < MaxDepth) {
				AB = A.Below; AA = A.Above; BB = B.Below; BA = B.Above;
				var ABB = AB.Intersect(B,depth,bound);
				var AAB = AA.Intersect(B,depth,bound);
				if (AAB && !ABB) { A = AA; goto Next; }
				if (ABB && !AAB) { A = AB; goto Next; }
				var BBA = BB.Intersect(A,depth,bound);
				var BAA = BA.Intersect(A,depth,bound);
				if (BAA && !BBA) { B = BA; goto Next; }
				if (BBA && !BAA) { B = BB; goto Next; }

				if (ABB && AAB) { A = AB; goto Next; }
				if (BBA && BAA) { B = BB; goto Next; }
			}
			Aref = A;
			Bref = B;
		}
		#endregion
		#region #method# IntersectEnd(Aref, Bref, bound) 
		private static bool IntersectEnd(ref Bezier Aref,ref Bezier Bref,int bound) {
			var A = Aref;
			var B = Bref;
			if (A.Intersect(B)) {
				Chance CA = A;
				Chance CB = B;
				while (A.Depth < MaxDepth || B.Depth < MaxDepth) {
					Chance TA = CA;
					while (TA != null) {
						Chance TB = CB;
						while (TB != null) {
							TA.Test(TB);
							TB = TB.Next;
						}
						TA = TA.Next;
					}
					if (A.Depth <= MaxDepth) CA = CA.Exists(bound);
					if (B.Depth <= MaxDepth) CB = CB.Exists(bound);
					if (CA != null && CB != null) {
						A = CA.Line;
						B = CB.Line;
					} else { break; }
				}
				Aref = A;
				Bref = B;
				return true;
			}
			return false;
		}
		#endregion
		#region #method# Correction0(A, B, Aout, Bout) 
		private static double Correction0(Bezier A,Bezier B,out Bezier Aout,out Bezier Bout) {
			Bezier AB, AA, BB, BA; bool O;
			do {
				O = false;
				AA = A.OtherGreater(B);
				if (AA != null) { A = AA; O = true; }
				BB = B.OtherGreater(A);
				if (BB != null) { B = BB; O = true; }
			} while (O);
			do {
				O = false;
				AB = A.Above;
				AA = A.Below;
				BB = B.Above;
				BA = B.Below;
				var abl = AB.Len(B);
				var aal = AA.Len(B);
				var bbl = BB.Len(A);
				var bal = BA.Len(A);
				var al = abl < aal ? abl : aal;
				var bl = bbl < bal ? bbl : bal;
				if (al < bl || bbl == bal) {
					if (abl < aal) { A = AB; O = true; }
					if (abl > aal) { A = AA; O = true; }
				}
				if (al > bl || abl == aal) {
					if (bbl < bal) { B = BB; O = true; }
					if (bbl > bal) { B = BA; O = true; }
				}
			} while (O);
			Aout = A;
			Bout = B;
			return A.Len(B);
		}
		#endregion
		#region #method# Correction1(A, B, Aout, Bout) 
		private static double Correction1(Bezier A,Bezier B,out Bezier Aout,out Bezier Bout) {
			Bezier AB, AA, BB, BA; bool O;
			do {
				O = false;
				BB = B.OtherGreater(A);
				if (BB != null) {
					B = BB; O = true;
				}
			} while (O);
			do {
				O = false;
				BB = B.Above;
				BA = B.Below;
				var bbl = BB.Len(A);
				var bal = BA.Len(A);
				if (bbl < bal) { B = BB; O = true; }
				if (bbl > bal) { B = BA; O = true; }
			} while (O);
			do {
				O = false;
				AB = A.Above;
				AA = A.Below;
				var abl = AB.Len(B);
				var aal = AA.Len(B);
				if (abl < aal) { A = AB; O = true; }
				if (abl > aal) { A = AA; O = true; }
			} while (O);
			Aout = A;
			Bout = B;
			return A.Len(B);
		}
		#endregion
		#region #method# Correction2(A, B, Aout, Bout) 
		private static double Correction2(Bezier A,Bezier B,out Bezier Aout,out Bezier Bout) {
			Bezier AB, AA, BB, BA; bool O;
			do {
				O = false;
				AA = A.OtherGreater(B);
				if (AA != null) { A = AA; O = true; }
			} while (O);
			do {
				O = false;
				AB = A.Above;
				AA = A.Below;
				var abl = AB.Len(B);
				var aal = AA.Len(B);
				if (abl < aal) { A = AB; O = true; }
				if (abl > aal) { A = AA; O = true; }
			} while (O);
			var ASB = B;
			do {
				O = false;
				BB = B.Above;
				BA = B.Below;
				var bbl = BB.Len(A);
				var bal = BA.Len(A);
				if (bbl < bal) { B = BB; O = true; }
				if (bbl > bal) { B = BA; O = true; }
			} while (O);
			Aout = A;
			Bout = B;
			return A.Len(B);
		}
		#endregion
		#region #method# PIntersect(A, #ref#AR, #ref#AX, #ref#AY, B, #ref#BR, #ref#BX, #ref#BY) 
		/// <summary>Возвращает истину если безьеры пересекаются и пересечения)</summary>
		/// <param name="A">Первый безьер)</param>
		/// <param name="AR">Корень расположения в первом безьере)</param>
		/// <param name="AX">Координата расположения пересечения X)</param>
		/// <param name="AY">Координата расположения пересечения Y)</param>
		/// <param name="B">Второй безьер)</param>
		/// <param name="BR">Корень расположения во втором безьере)</param>
		/// <param name="BX">Координата расположения пересечения X)</param>
		/// <param name="BY">Координата расположения пересечения Y)</param>
		/// <returns>Возвращает истину если пересечения найдены)</returns>
		public static bool PIntersect(Bezier A,ref double AR,ref double AX,ref double AY,Bezier B,ref double BR,ref double BX,ref double BY) {
			bool O;
			A = A.Pastle;
			B = B.Pastle;
			var depth = PresetDepthMin;
			var Abak = A;
			var Bbak = B;
			Bezier PA = null, PB = null;
			var PL = double.MaxValue;
			if (A.Intersect(B)) {
			Next:
				IntersectFor(ref A,ref B,depth,PresetExistsMax);
				IntersectEnd(ref A,ref B,PresetExistsMax);
				var L0 = Correction0(A,B,out var A0,out var B0);
				var L1 = Correction1(A,B,out var A1,out var B1);
				var L2 = Correction2(A,B,out var A2,out var B2);
				if (L0 < L1 && L0 < L2) {
					if (PL > L0) { PL = L0; PA = A0; PB = B0; }
				} else if (L1 < L2) {
					if (PL > L1) { PL = L0; PA = A1; PB = B1; }
				} else {
					if (PL > L2) { PL = L2; PA = A2; PB = B2; }
				}
				var PAR = System.Math.Round(PA.Root,1);
				var PBR = System.Math.Round(PB.Root,1);
				Abak.Get(PAR,out var PAX,out var PAY);
				Bbak.Get(PBR,out var PBX,out var PBY);
				L0 = Len(PAX - PBX,PAY - PBY);
				L1 = Len(PA.X - PBX,PA.Y - PBY);
				L2 = Len(PAX - PB.X,PAY - PB.Y);
				if (L0 <= PL && L0 <= L1 && L0 <= L2) {
					PL = L0;
					AR = PAR; AX = PAX; AY = PAY;
					BR = PBR; BX = PBX; BY = PBY;
				} else if (L1 <= PL && L1 <= L2) {
					PL = L1;
					AR = PA.Root; AX = PA.X; AY = PA.Y;
					BR = PBR; BX = PBX; BY = PBY;
				} else if (L2 <= PL) {
					PL = L2;
					AR = PAR; AX = PAX; AY = PAY;
					BR = PB.Root; BX = PB.X; BY = PB.Y;
				} else {
					AR = PA.Root; AX = PA.X; AY = PA.Y;
					BR = PB.Root; BX = PB.X; BY = PB.Y;
				}
				if (PL <= PresetLengthMin) {
					if (AR == 0.0 || AR == 1.0) { BX = AX; BY = AY; } else { AX = BX; AY = BY; }
					return true;
				}
				if (depth < PresetDepthMax) {
					depth++;
					A = Abak;
					B = Bbak;
					goto Next;
				}
			}
			return false;
		}
		#endregion
		#region #method# PIntersect(#ref#Aref, #ref#Bref) 
		/// <summary>Возвращает истину если безьеры пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый безьер)</param>
		/// <param name="Bref">Второй безьер)</param>
		/// <returns>Возвращает истину если безьеры пересекаются или ложь)</returns>
		public static bool PIntersect(ref Bezier Aref,ref Bezier Bref) {
			bool O;
			var A = Aref.Pastle;
			var B = Bref.Pastle;
			var depth = PresetDepthMin;
			var Abak = A;
			var Bbak = B;
			Bezier PA = null, PB = null;
			var PL = double.MaxValue;
			if (A.Intersect(B)) {
			Next:
				IntersectFor(ref A,ref B,depth,PresetExistsMax);
				IntersectEnd(ref A,ref B,PresetExistsMax);
				var L0 = Correction0(A,B,out var A0,out var B0);
				var L1 = Correction1(A,B,out var A1,out var B1);
				var L2 = Correction2(A,B,out var A2,out var B2);
				if (L0 < L1 && L0 < L2) {
					if (PL > L0) { PL = L0; PA = A0; PB = B0; }
				} else if (L1 < L2) {
					if (PL > L1) { PL = L0; PA = A1; PB = B1; }
				} else {
					if (PL > L2) { PL = L2; PA = A2; PB = B2; }
				}
				if (PL <= PresetLengthMin) {
					var PAR = System.Math.Floor(PA.Root);
					var PBR = System.Math.Floor(PB.Root);
					Abak.Get(PAR,out var PAX,out var PAY);
					Abak.Get(PAR,out var PBX,out var PBY);
					Aref = PA;
					Bref = PB;
					return true;
				}
				if (depth < PresetDepthMax) {
					depth++;
					A = Abak;
					B = Bbak;
					goto Next;
				}
			}
			return false;
		}
		#endregion
		#region #method# Intersect(A, #ref#AR, #ref#AX, #ref#AY, B, #ref#BR, #ref#BX, #ref#BY, Lmin, Dmin, Dmax, bound) 
		/// <summary>Возвращает истину если безьеры пересекаются и пересечения)</summary>
		/// <param name="A">Первый безьер)</param>
		/// <param name="AR">Корень расположения в первом безьере)</param>
		/// <param name="AX">Координата расположения пересечения X)</param>
		/// <param name="AY">Координата расположения пересечения Y)</param>
		/// <param name="B">Второй безьер)</param>
		/// <param name="BR">Корень расположения во втором безьере)</param>
		/// <param name="BX">Координата расположения пересечения X)</param>
		/// <param name="BY">Координата расположения пересечения Y)</param>
		/// <param name="Lmin">Минимальное растояние между точками)</param>
		/// <param name="Dmin">Минимальная глубина)</param>
		/// <param name="Dmax">Максимальная глубина)</param>
		/// <param name="bound">Ограничение сравнения с краев шанса)</param>
		/// <returns>Возвращает истину если пересечения найдены)</returns>
		public static bool Intersect(Bezier A,ref double AR,ref double AX,ref double AY,Bezier B,ref double BR,ref double BX,ref double BY,double Lmin = 0.1,int Dmin = 5,int Dmax = 10,int bound = 8) {
			bool O;
			A = A.Pastle;
			B = B.Pastle;
			var depth = Dmin;
			var Abak = A;
			var Bbak = B;
			Bezier PA = null, PB = null;
			var PL = double.MaxValue;
			if (A.Intersect(B)) {
			Next:
				IntersectFor(ref A,ref B,depth,bound);
				IntersectEnd(ref A,ref B,bound);
				var L0 = Correction0(A,B,out var A0,out var B0);
				var L1 = Correction1(A,B,out var A1,out var B1);
				var L2 = Correction2(A,B,out var A2,out var B2);
				if (L0 < L1 && L0 < L2) {
					if (PL > L0) { PL = L0; PA = A0; PB = B0; }
				} else if (L1 < L2) {
					if (PL > L1) { PL = L0; PA = A1; PB = B1; }
				} else {
					if (PL > L2) { PL = L2; PA = A2; PB = B2; }
				}
				var PAR = System.Math.Round(PA.Root,1);
				var PBR = System.Math.Round(PB.Root,1);
				Abak.Get(PAR,out var PAX,out var PAY);
				Bbak.Get(PBR,out var PBX,out var PBY);
				L0 = Len(PAX - PBX,PAY - PBY);
				L1 = Len(PA.X - PBX,PA.Y - PBY);
				L2 = Len(PAX - PB.X,PAY - PB.Y);
				if (L0 <= PL && L0 <= L1 && L0 <= L2) {
					PL = L0;
					AR = PAR; AX = PAX; AY = PAY;
					BR = PBR; BX = PBX; BY = PBY;
				} else if (L1 <= PL && L1 <= L2) {
					PL = L1;
					AR = PA.Root; AX = PA.X; AY = PA.Y;
					BR = PBR; BX = PBX; BY = PBY;
				} else if (L2 <= PL) {
					PL = L2;
					AR = PAR; AX = PAX; AY = PAY;
					BR = PB.Root; BX = PB.X; BY = PB.Y;
				} else {
					AR = PA.Root; AX = PA.X; AY = PA.Y;
					BR = PB.Root; BX = PB.X; BY = PB.Y;
				}
				if (PL <= Lmin) {
					if (AR == 0.0 || AR == 1.0) { BX = AX; BY = AY; } else { AX = BX; AY = BY; }
					return true;
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
		#region #method# Intersect(#ref#Aref, #ref#Bref, Aend, Bnot, Lmin, Dmin, Dmax, bound) 
		/// <summary>Возвращает истину если безьеры пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый безьер)</param>
		/// <param name="Bref">Второй безьер)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="Dmin">Минимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="bound">Ограничитель разбора при заглублении, значение меньше 2 отключает ограничение)</param>
		/// <returns>Возвращает истину если безьеры пересекаются или ложь)</returns>
		public static bool Intersect(ref Bezier Aref,ref Bezier Bref,bool Aend = false,bool Bnot = false,double Lmin = 0.01,int Dmin = 7,int Dmax = 12,int bound = 8) {
			bool O;
			var A = Aref.Pastle;
			if (Aend) A = A.Return;
			var B = Bref.Pastle;
			if (Aend ^ Bnot) B = B.Return;
			var depth = Dmin;
			var Abak = A;
			var Bbak = B;
			Bezier PA = null, PB = null;
			var PL = double.MaxValue;
			if (A.Intersect(B)) {
			Next:
				IntersectFor(ref A,ref B,depth,bound);
				IntersectEnd(ref A,ref B,bound);
				var L0 = Correction0(A,B,out var A0,out var B0);
				var L1 = Correction1(A,B,out var A1,out var B1);
				var L2 = Correction2(A,B,out var A2,out var B2);
				if (L0 < L1 && L0 < L2) {
					if (PL > L0) { PL = L0; PA = A0; PB = B0; }
				} else if (L1 < L2) {
					if (PL > L1) { PL = L0; PA = A1; PB = B1; }
				} else {
					if (PL > L2) { PL = L2; PA = A2; PB = B2; }
				}
				if (PL <= Lmin) {
					var PAR = System.Math.Floor(PA.Root);
					var PBR = System.Math.Floor(PB.Root);
					Abak.Get(PAR,out var PAX,out var PAY);
					Abak.Get(PAR,out var PBX,out var PBY);
					Aref = PA;
					Bref = PB;
					return true;
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
		#region #method# IntersectTest(#ref#Aref, #ref#Bref, Aend, Bnot, Lmin, Dmin, Dmax, bound) 
		/// <summary>Возвращает истину если безьеры пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый безьер)</param>
		/// <param name="Bref">Второй безьер)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="Dmin">Минимальная глубина сравнения)</param>
		/// <param name="Dmax">Максимальная глубина сравнения)</param>
		/// <param name="bound">Ограничитель разбора при заглублении, значение меньше 2 отключает ограничение)</param>
		/// <returns>Растояние между пересечениями безьеров)</returns>
		public static double IntersectTest(ref Bezier Aref,ref Bezier Bref,bool Aend = false,bool Bnot = false,double Lmin = 0.01,int Dmin = 7,int Dmax = 12,int bound = 4) {
			bool O;
			var A = Aref.Pastle;
			if (Aend) A = A.Return;
			var B = Bref.Pastle;
			if (Aend ^ Bnot) B = B.Return;
			var depth = Dmin;
			var Abak = A;
			var Bbak = B;
			Bezier PA = null, PB = null;
			var PL = double.MaxValue;
			if (A.Intersect(B)) {
			Next:
				IntersectFor(ref A,ref B,depth,bound);
				IntersectEnd(ref A,ref B,bound);
				var L0 = Correction0(A,B,out var A0,out var B0);
				var L1 = Correction1(A,B,out var A1,out var B1);
				var L2 = Correction2(A,B,out var A2,out var B2);
				if (L0 < L1 && L0 < L2) {
					if (PL > L0) { PL = L0; PA = A0; PB = B0; }
				} else if (L1 < L2) {
					if (PL > L1) { PL = L0; PA = A1; PB = B1; }
				} else {
					if (PL > L2) { PL = L2; PA = A2; PB = B2; }
				}
				if (PL <= Lmin || depth == Dmax) {
					Aref = PA;
					Bref = PB;
					return PL;
				}
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
			return $"L x0={(this.x0).ToString("R",I)} y0={(this.y0).ToString("R",I)} x1={(this.x1).ToString("R",I)} y1={(this.y1).ToString("R",I)}";
		}
		#endregion
		#region #invisible# #get# Pastle 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Bezier Pastle {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => new Bezier(x0,y0,x1,y1,Not: this.Not);
		}
		#endregion
		#region #invisible# #get# Return 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Bezier Return {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get => new Bezier(x0,y0,x1,y1,Not: !this.Not);
		}
		#endregion
		#region #invisible# #get# Invert 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Bezier Invert {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				return new Bezier(x1,y1,x0,y0,Not: !this.Not);
			}
		}
		#endregion
		#region #invisible# #get# Incest 
		#region #invisible# 
#if TRACE
		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
		#endregion
		public virtual Bezier Incest {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				return new Bezier(x1,y1,x0,y0,Not: this.Not);
			}
		}
		#endregion
		#region #method# Neq(B) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public bool Neq(Bezier B) {
			return (this.x0 != B.x0 || this.y0 != B.y0) && (this.x1 != B.x1 || this.y1 != B.y1) && (this.x0 != B.x1 || this.y0 != B.y1) && (this.x1 != B.x0 || this.y1 != B.y0);
		}
		#endregion
		#region #method# Seq(B) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public virtual bool Seq(Bezier B) {
			return (this.x0 == B.x0 && this.y0 == B.y0) && (this.x1 == B.x1 && this.y1 == B.y1);
		}
		#endregion
		#region #method# Meq(B) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public virtual bool Meq(Bezier B) {
			return (this.x0 == B.x1 && this.y0 == B.y1) && (this.x1 == B.x0 && this.y1 == B.y0);
		}
		#endregion
		#region #method# Equ(B) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public virtual bool Equ(Bezier B) {
			return (this.x0 == B.x0 && this.y0 == B.y0) && (this.x1 == B.x1 && this.y1 == B.y1);
		}
		#endregion
		#region #method# Equ(B) 
		public static bool Equ(Bezier A0,Bezier A1,Bezier B0,Bezier B1) {
			if (A1.x1 == B0.x0 && A1.y1 == B0.y0) {
				var A = new Bezier(A0.x1,A0.y1,A1.x0,A1.y0);
				var B = new Bezier(B0.x1,B0.y1,B1.x0,B1.y0);
				if (PIntersect(ref A,ref B)) return true;
			}
			return false;
		}
		#endregion
		#region #property# Line 
		public virtual Wins.PathSource.Line Line {
			get { return new Wins.PathSource.Line(x0,y0,x1,y1); }
		}
		#endregion
	}
	#endregion
}
