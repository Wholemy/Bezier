namespace Wholemy {
	public class Bzier {
		public const int MinMaxS = 192;
		public const int MaxDepth = 32;
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
		#region #class# Hot 
		public sealed class Hot {
			public readonly Lot Lot;
			public readonly Dot Dot;
			public readonly Dir Dir;
			public readonly Hot Prev;
			public readonly int PrevCount;
			public int NextCount;
			public Hot Next;
			#region #new# (Dot, Max) 
			public Hot(Dot Dot, Dir Dir, int PrevCount, int NextCount) {
				#region #debug# 
#if DEBUG
				if(Dot == null || Dot.Hot != null || Dot.Lot == null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Dot = Dot;
				Dot.Hot = this;
				this.PrevCount = PrevCount;
				this.NextCount = NextCount;
				this.Dir = Dir;
				var Lot = this.Lot = Dot.Lot; var Last = Lot.HotLast;
				if(Last != null) { Last.Next = this; this.Prev = Last; } else { Lot.HotBase = this; }
				Lot.HotLast = this; Lot.HotCache = null; Lot.HotCount++;
			}
			#endregion
			public override string ToString() {
				return $"{this.PrevCount.ToString()} {Dir.ToString()} {this.NextCount.ToString()} {Dot.ToString()}";
			}
		}
		#endregion
		#region #class# Lot 
		public class Lot {
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public int DotCount;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Dot DotBase;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Dot DotLast;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public int HotCount;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public int MinCount;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public int MaxCount;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Hot HotBase;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Hot HotLast;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Dot[] DotCache;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public Hot[] HotCache;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public readonly Line Line;
			#region #invisible# 
#if TRACE
			[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
#endif
			#endregion
			public double Size;
			#region #new# (Line) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Lot(Line Line) {
				this.Line = Line;
				Line.Dot(0.0).LastTo(this);
				Line.Dot(0.5).LastTo(this);
				Line.Dot(1.0).LastTo(this);
				Size = 0.5;
				//var S = 1.0 / 256;
				//var R = 0.0;
				//while(R < 1.0) { Line.Dot(R).LastTo(this); R += S; }
				//Size = S;
			}
			#endregion
			#region #property# Dots 
			public Dot[] Dots {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if(DotCache != null) return DotCache;
					var I = DotCount;
					var A = new Dot[I];
					var S = DotLast;
					while(--I >= 0) {
						A[I] = S;
						S = S.Prev;
					}
					DotCache = A;
					return A;
				}
			}
			#endregion
			#region #property# Hots 
			public Hot[] Hots {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get {
					if(HotCache != null) return HotCache;
					var I = HotCount;
					var A = new Hot[I];
					var S = HotLast;
					while(--I >= 0) {
						A[I] = S;
						S = S.Prev;
					}
					HotCache = A;
					return A;
				}
			}
			#endregion
			#region #method# ToString 
			public override string ToString() {
				var I = System.Globalization.CultureInfo.InvariantCulture;
				return $"List Count:{DotCount.ToString(I)} Hots:{HotCount.ToString(I)} Mins:{MinCount.ToString(I)} Maxs:{MaxCount.ToString(I)}";
			}
			#endregion
			#region #method# Cut 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public void Cut() {
				int C = 0;
				var I = DotBase;
				while(I != null) {
					var Next = I.Next;
					if(!I.Int) { I.Cut(); }
					I = Next;
				}
			}
			#endregion
			#region #method# Len(A, B) 
			public static void Len(Lot A, Lot B) {
				var I = A.DotBase;
				while(I != null) {
					var ii = B.DotBase;
					while(ii != null) {
						var LT = ii.LenTo(I);
						if(double.IsNaN(I.Len) || LT < I.Len) { I.Len = LT; I.Let = ii; }
						if(double.IsNaN(ii.Len) || LT < ii.Len) { ii.Len = LT; ii.Let = I; }
						ii = ii.Next;
					}
					I = I.Next;
				}
				I = B.DotBase;
				while(I != null) {
					var ii = A.DotBase;
					while(ii != null) {
						var LT = ii.LenTo(I);
						if(double.IsNaN(I.Len) || LT < I.Len) { I.Len = LT; I.Let = ii; }
						if(double.IsNaN(ii.Len) || LT < ii.Len) { ii.Len = LT; ii.Let = I; }
						ii = ii.Next;
					}
					I = I.Next;
				}
			}
			#endregion
			public void MinMaxFirst(double Mlen, int Mmax) {
				double MinLen = double.NaN, MaxLen = double.NaN;
				Dot MinDot = null, MaxDot = null;
				var Hot = this.HotBase;
				while(Hot != null) {
					Hot.Dot.Hot = null;
					Hot = Hot.Next;
				}
				this.HotBase = null;
				this.HotLast = null;
				this.HotCount = 0;
				this.DotCache = null;
				Hot H = null;
				Hot E = null;
				var P = this.DotBase;
				if(P != null) {
					var I = P.Next;
					while(I != null) {
						var Len = I.Len;
						if(P != null) {
							if(H == null) {
								if(E != null) {
									if(P.Len < Len) {
										H = new Hot(P, Dir.Min, E.NextCount, 1);
										E = null;
									} else if(P.Len > Len) {
										H = new Hot(P, Dir.Max, E.NextCount, 1);
										E = null;
									} else {
										E.NextCount++; ;
									}
								} else {
									if(P.Len < Len) {
										H = new Hot(P, Dir.Min, 0, 1);
									} else if(P.Len > Len) {
										H = new Hot(P, Dir.Max, 0, 1);
									} else {
										E = new Hot(P, Dir.Equ, 0, 1);
									}
								}
							} else {
								if(E != null) {
									if(P.Len < Len) {
										switch(H.Dir) {
										case Dir.Min: new Hot(P, Dir.Equ, E.NextCount, 1); break;
										case Dir.Max: H = new Hot(P, Dir.Min, E.NextCount, 1); break;
										}
										E = null;
									} else if(P.Len > Len) {
										switch(H.Dir) {
										case Dir.Min: H = new Hot(P, Dir.Max, E.NextCount, 1); break;
										case Dir.Max: new Hot(P, Dir.Equ, E.NextCount, 1); break;
										}
										E = null;
									} else {
										E.NextCount++;
									}
								} else {
									if(P.Len < Len) {
										switch(H.Dir) {
										case Dir.Min: H.NextCount++; break;
										case Dir.Max: H = new Hot(P, Dir.Min, H.NextCount, 1); break;
										}
									} else if(P.Len > Len) {
										switch(H.Dir) {
										case Dir.Max: H.NextCount++; break;
										case Dir.Min: H = new Hot(P, Dir.Max, H.NextCount, 1); break;
										}
									} else {
										E = new Hot(P, Dir.Equ, H.NextCount, 1); break;
									}
								}
							}
						}
						P = I; I = I.Next;
					}
					if(H != null) {
						if(E != null) {
							switch(H.Dir) {
							case Dir.Min: new Hot(P, Dir.Max, E.NextCount, 0); break;
							case Dir.Max: new Hot(P, Dir.Min, E.NextCount, 0); break;
							}
						} else {
							switch(H.Dir) {
							case Dir.Min: new Hot(P, Dir.Max, H.NextCount, 0); break;
							case Dir.Max: new Hot(P, Dir.Min, H.NextCount, 0); break;
							}
						}
					} else {
						if(E != null) {
							new Hot(P, Dir.Equ, E.NextCount, 0);
						}
					}
				}
			}
			#region #method# Enter(P) 
			public void Enter(Lot P) {
				this.Reset();
				P.Reset();
			}
			#endregion
			#region #method# Reset 
			private void Reset() {
				var I = this.DotBase;
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
			#endregion
			#region #method# Dep 
			public void Dep() {
				Size *= 0.5;
				var I = DotBase;
				if(I != null && I.Root >= Size) {
					Line.Dot(I.Root - Size).PrevTo(I);
				}
				while(I != null) {
					var N = I.Next;
					if(N != null) {
						var R = (N.Root - I.Root) / 2;
						//if(I.Root + Size < N.Root) {
						I = Line.Dot(I.Root + R).NextTo(I);
						//}
					} else {
						if(I.Root + Size <= 1.0) {
							Line.Dot(I.Root + Size).NextTo(I);
						}
					}
					I = N;
				}
			}
			#endregion
			#region #method# Get 
			public Line Get() {
				var I = this.DotBase;
				if(I != null) {
					var L = I.Len;
					var M = I;
					while(I != null) {
						var ll = I.Len;
						if(ll < L) { L = ll; M = I; }
						I = I.Next;
					}
					return Line.DivB(M.Root).DivA(0.0);
				}
				return null;
			}
			#endregion
		}
		#endregion
		public enum Dir {
			Min = 0,
			Equ = 1,
			Max = 2
		}
		#region #class# Dot 
		public class Dot {
			public Lot Lot;
			public Dot Prev;
			public Dot Next;
			#region #property# Let 
			private System.WeakReference<Dot> let;
			public Dot Let {
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				get { if(this.let != null) { if(this.let.TryGetTarget(out var let)) { return let; } else { this.let = null; } } return null; }
				#region #through# 
#if TRACE
				[System.Diagnostics.DebuggerStepThrough]
#endif
				#endregion
				set { if(value != null) { this.let = new System.WeakReference<Dot>(value, false); } else { this.let = null; } }
			}
			#endregion
			public Hot Hot;
			public double Len = double.NaN;
			public bool Int = true;
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
				return $"Dot {(Int ? "Int" : "Out")} Len:{Len.ToString(I)} Root:{Root.ToString(I)} X:{X.ToString(I)} Y:{Y.ToString(I)}";
			}
			#endregion
			#region #method# LenTo(Dot) 
			public double LenTo(Dot Dot) {
				var X = this.X - Dot.X;
				var Y = this.Y - Dot.Y;
				return System.Math.Sqrt(X * X + Y * Y);
			}
			#endregion
			#region #method# Cut 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot Cut() {
				if(Lot == null) throw new System.InvalidOperationException();
				if(Prev != null) { Prev.Next = Next; } else { Lot.DotBase = Next; }
				if(Next != null) { Next.Prev = Prev; } else { Lot.DotLast = Prev; }
				Prev = null;
				Next = null;
				Lot.DotCache = null;
				Lot.DotCount--;
				Lot = null;
				return this;
			}
			#endregion
			#region #method# LastTo(Lot) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot LastTo(Lot Lot) {
				#region #debug# 
#if DEBUG
				if(Lot == null || this.Lot != null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Lot = Lot;
				if(Lot.DotCount > 0) {
					this.Prev = Lot.DotLast;
					Lot.DotLast.Next = this;
					Lot.DotLast = this;
				} else {
					Lot.DotBase = Lot.DotLast = this;
				}
				Lot.DotCache = null;
				Lot.DotCount++;
				return this;
			}
			#endregion
			#region #method# BaseTo(Lot) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot BaseTo(Lot Lot) {
				#region #debug# 
#if DEBUG
				if(Lot == null || this.Lot != null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Lot = Lot;
				if(Lot.DotCount > 0) {
					this.Next = Lot.DotBase;
					Lot.DotBase.Prev = this;
					Lot.DotBase = this;
				} else {
					Lot.DotBase = Lot.DotLast = this;
				}
				Lot.DotCache = null;
				Lot.DotCount++;
				return this;
			}
			#endregion
			#region #method# PrevTo(Dot) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot PrevTo(Dot Dot) {
				#region #debug# 
#if DEBUG
				if(Dot == null || Dot.Lot == null || this.Lot != null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Lot = Dot.Lot;
				this.Next = Dot;
				var Prev = Dot.Prev;
				Dot.Prev = this;
				if(Prev != null) {
					this.Prev = Prev;
					Prev.Next = this;
				} else {
					this.Lot.DotBase = this;
				}
				this.Lot.DotCache = null;
				this.Lot.DotCount++;
				return this;
			}
			#endregion
			#region #method# NextTo(Dot) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot NextTo(Dot Dot) {
				#region #debug# 
#if DEBUG
				if(Dot == null || Dot.Lot == null || this.Lot != null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Lot = Dot.Lot;
				this.Prev = Dot;
				var Next = Dot.Next;
				Dot.Next = this;
				if(Next != null) {
					this.Next = Next;
					Next.Prev = this;
				} else {
					this.Lot.DotLast = this;
				}
				this.Lot.DotCache = null;
				this.Lot.DotCount++;
				return this;
			}
			#endregion
			#region #method# Replace(Dot) 
			#region #through# 
#if TRACE
			[System.Diagnostics.DebuggerStepThrough]
#endif
			#endregion
			public Dot Replace(Dot Dot) {
				#region #debug# 
#if DEBUG
				if(Dot == null || Dot.Lot == null || this.Lot != null) throw new System.InvalidOperationException();
#endif
				#endregion
				this.Lot = Dot.Lot;
				this.Prev = Dot.Prev;
				this.Next = Dot.Next;
				if(this.Prev != null) { this.Prev.Next = this; } else { this.Lot.DotBase = this; }
				if(this.Next != null) { this.Next.Prev = this; } else { this.Lot.DotLast = this; }
				Dot.Prev = null;
				Dot.Next = null;
				Dot.Lot = null;
				this.Lot.DotCache = null;
				return this;
			}
			#endregion
			#region #method# Reset 
			public void Reset() {
				var Lot = this.Lot;
				var C = MinMaxS;
				this.Int = true;
				var R = this.Prev;
				while(R != null) {
					if(C > 0) {
						R.Int = true;
						C--;
					} else {
						R.Int = false;
					}
					R = R.Prev;
				}
				if(Lot.DotBase.Root > 0.0 && C > 0) {
					var S = Lot.Size;//Lot.Base.Root / C;
					while(C > 0) {
						if(Lot.DotBase.Root > 0.0) {
							S = Lot.DotBase.Root - S; if(S < 0.0) S = 0.0;
							Lot.Line.Dot(S).BaseTo(Lot);
						}
						C--;
					}
				}
				C = MinMaxS;
				R = this.Next;
				while(R != null) {
					if(C > 0) {
						R.Int = true;
						C--;
					} else {
						R.Int = false;
					}
					R = R.Next;
				}
				if(Lot.DotLast.Root < 1.0 && C > 0) {
					var S = Lot.Size;//Lot.Last.Root / C;
					while(C > 0) {
						if(Lot.DotLast.Root < 1.0) {
							S = Lot.DotBase.Root + S; if(S > 1.0) S = 1.0;
							Lot.Line.Dot(Lot.DotBase.Root + S).LastTo(Lot);
						}
						C--;
					}
				}
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
		#region #method# Intersect2L(Aref, Bref, Mlen) 
		public static double Intersect2L(ref Line Aref, ref Line Bref, double Mlen = 0.25) {
			var A = Aref.Pastle;
			var B = Bref.Pastle;
			var D = 0;
			var M = Mlen;
			if(A.Intersect(B)) {
				var Mint = A.Mint * B.Mint;
				var AP = new Lot(A);
				var BP = new Lot(B);
				Lot.Len(AP, BP);
				AP.MinMaxFirst(Mlen, Mint);
				BP.MinMaxFirst(Mlen, Mint);
				do {
					AP.Dep();
					BP.Dep();
					Lot.Len(AP, BP);
					AP.MinMaxFirst(Mlen, Mint);
					BP.MinMaxFirst(Mlen, Mint);
					AP.Enter(BP);
					AP.Cut();
					BP.Cut();
					D++;
				} while(D < MaxDepth);
				A = AP.Get();
				B = BP.Get();
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
