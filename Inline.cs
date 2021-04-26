namespace Wholemy {
	#region #class# Inline 
	public class Inline {
		#region #class# Quadratic 
		public class Quadratic : Inline {
			public double x2;
			public double y2;
			#region #new# (x0, y0, x1, y1, x2, y2, S = 1.0, I = 0.5) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Quadratic(double x0, double y0, double x1, double y1, double x2, double y2, double S = 0.5, double I = 0.5, Inline O = null) {
				if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
				this.Parent = O;
				this.x0 = x0;
				this.y0 = y0;
				this.x1 = x2;
				this.y1 = y2;
				this.x2 = x1;
				this.y2 = y1;
				this.Root = I;
				this.Size = S;
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
				var x11 = x1;
				var y11 = y1;
				var x22 = x2;
				var y22 = y2;
				var x01 = (x11 - x00) * 0.5 + x00;
				var y01 = (y11 - y00) * 0.5 + y00;
				var x12 = (x22 - x11) * 0.5 + x11;
				var y12 = (y22 - y11) * 0.5 + y11;
				var x02 = (x01 - x12) * 0.5 + x12;
				var y02 = (y01 - x12) * 0.5 + x12;
				X = x02;
				Y = y02;
			}
			#endregion
			#region #method# Division 
			protected override void Divisions() {
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
				var x02 = (x01 - x12) * 0.5 + x12;
				var y02 = (y01 - x12) * 0.5 + x12;
				this.X = x02;
				this.Y = y02;
				var S = 0.5 * this.Size;
				if (this.belowb == null)
					this.belowb = new Quadratic(x00, y00, x01, y01, x02, y02, S, this.Root - S, this);
				if (this.aboveb == null)
					this.aboveb = new Quadratic(x02, y02, x12, y12, x22, y22, this.Size - S, this.Root + S, this);
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
		}
		#endregion
		#region #class# Cubic 
		public class Cubic : Inline {
			public double x2;
			public double y2;
			public double x3;
			public double y3;
			#region #new# (x0, y0, x1, y1, x2, y2, x3, y3, S = 1.0, I = 0.0) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Cubic(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, double S = 0.5, double I = 0.5, Inline O = null) {
				var Depth = 0;
				if (O != null) Depth = O.Depth + 1;
				this.Depth = Depth;
				this.Parent = O;
				this.x0 = x0;
				this.y0 = y0;
				this.x1 = x3;
				this.y1 = y3;
				this.x2 = x1;
				this.y2 = y1;
				this.x3 = x2;
				this.y3 = y2;
				this.Root = I;
				this.Size = S;
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
				var x00 = x0;
				var y00 = y0;
				var x11 = x1;
				var y11 = y1;
				var x22 = x2;
				var y22 = y2;
				var x33 = x3;
				var y33 = y3;
				var x01 = (x11 - x00) * 0.5 + x00;
				var y01 = (y11 - y00) * 0.5 + y00;
				var x12 = (x22 - x11) * 0.5 + x11;
				var y12 = (y22 - y11) * 0.5 + y11;
				var x23 = (x33 - x22) * 0.5 + x22;
				var y23 = (y33 - y22) * 0.5 + y22;
				var x02 = (x12 - x01) * 0.5 + x01;
				var y02 = (y12 - y01) * 0.5 + y01;
				var x13 = (x23 - x12) * 0.5 + x12;
				var y13 = (y23 - y12) * 0.5 + y12;
				var x03 = (x13 - x02) * 0.5 + x02;
				var y03 = (y13 - y02) * 0.5 + y02;
				X = x03;
				Y = y03;
			}
			#endregion
			#region #method# Division 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			protected override void Divisions() {
				var x00 = x0;
				var y00 = y0;
				var x11 = x2;
				var y11 = y2;
				var x22 = x3;
				var y22 = y3;
				var x33 = x1;
				var y33 = y1;
				var x01 = (x11 - x00) * 0.5 + x00;
				var y01 = (y11 - y00) * 0.5 + y00;
				var x12 = (x22 - x11) * 0.5 + x11;
				var y12 = (y22 - y11) * 0.5 + y11;
				var x23 = (x33 - x22) * 0.5 + x22;
				var y23 = (y33 - y22) * 0.5 + y22;
				var x02 = (x12 - x01) * 0.5 + x01;
				var y02 = (y12 - y01) * 0.5 + y01;
				var x13 = (x23 - x12) * 0.5 + x12;
				var y13 = (y23 - y12) * 0.5 + y12;
				var x03 = (x13 - x02) * 0.5 + x02;
				var y03 = (y13 - y02) * 0.5 + y02;
				this.X = x03;
				this.Y = y03;
				var S = 0.5 * this.Size;
				if (this.belowb == null)
					this.belowb = new Cubic(x00, y00, x01, y01, x02, y02, x03, y03, S, this.Root - S, this);
				if (this.aboveb == null)
					this.aboveb = new Cubic(x03, y03, x13, y13, x23, y23, x33, y33, this.Size - S, this.Root + S, this);
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
		}
		#endregion
		public double Size;
		public double L;
		public double T;
		public double R;
		public double B;
		public double x0;
		public double y0;
		public double x1;
		public double y1;
		public double Root;
		public double X;
		public double Y;
		public int Depth;
		private Inline Parent;
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
		public Inline Below {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.belowb == null) this.Divisions();
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
		public Inline Above {
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			get {
				if (this.aboveb == null) this.Divisions();
				return this.aboveb;
			}
		}
		#endregion
		#region #new# 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		private Inline() { }
		#endregion
		#region #new# (x0, y0, x1, y1, S = 1.0, I = 0.0) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public Inline(double x0, double y0, double x1, double y1, double S = 0.5, double I = 0.5, Inline O = null) {
			if (O != null) this.Depth = O.Depth + 1; else this.Depth = 0;
			this.Parent = O;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			this.Root = I;
			this.Size = S;
			if (x0 < x1) { L = x0; R = x1; } else { L = x1; R = x0; }
			if (y0 < y1) { T = y0; B = y1; } else { T = y1; B = y0; }
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * 0.5 + x00;
			var y01 = (y11 - y00) * 0.5 + y00;
			this.X = x01;
			this.Y = y01;
		}
		#endregion
		#region #method# Divisions 
		protected virtual void Divisions() {
			var x00 = x0;
			var y00 = y0;
			var x11 = x1;
			var y11 = y1;
			var x01 = (x11 - x00) * 0.5 + x00;
			var y01 = (y11 - y00) * 0.5 + y00;
			this.X = x01;
			this.Y = y01;
			var S = 0.5 * this.Size;
			if (this.belowb == null)
				this.belowb = new Inline(x00, y00, x01, y01, S, this.Root - S, this);
			if (this.aboveb == null)
				this.aboveb = new Inline(x01, y01, x11, y11, this.Size - S, this.Root + S, this);
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
		#region #method# Intersect(Aref, Bref, Aend, Bnot, Lmin = 0.5, depth = 5) 
		/// <summary>Возвращает истину если инлайны пересекаются и пересечения)</summary>
		/// <param name="Aref">Первый инлайн)</param>
		/// <param name="Bref">Второй инлайн)</param>
		/// <param name="Aend">Истина определяет поиск перечений с конца)</param>
		/// <param name="Bnot">Истина определяет обратный поиск от Aend)</param>
		/// <param name="Lmin">Минимальное растояние между пересечением)</param>
		/// <param name="depth">Глубина парного перебора в битвине)</param>
		/// <returns></returns>
		public static bool Intersect(ref Inline Aref, ref Inline Bref, bool Aend = false, bool Bnot = false, double Lmin = 0.5, int depth = 5) {
			var A = Aref;
			var B = Bref;
			var coutA = 0;
			var coutB = 0;
			Bnot = Aend ^ Bnot;
			if (A.Intersect(B)) {
			Next:
				if (coutA < 64 && coutB < 64) {
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
				bool O = true;
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
