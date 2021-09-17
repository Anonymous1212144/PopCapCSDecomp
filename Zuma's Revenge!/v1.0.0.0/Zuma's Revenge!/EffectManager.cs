using System;
using System.Collections.Generic;
using SexyFramework;

namespace ZumasRevenge
{
	public class EffectManager : IDisposable
	{
		public EffectManager()
		{
			this.Reset();
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mEffects.Count; i++)
			{
				if (this.mEffects[i] != null)
				{
					this.mEffects[i].Dispose();
					this.mEffects[i] = null;
				}
			}
			this.mEffects.Clear();
		}

		public void Reset()
		{
			if (GameApp.gApp != null && GameApp.gApp.mShutdown)
			{
				return;
			}
			for (int i = 0; i < this.mEffects.Count; i++)
			{
				if (this.mEffects[i] != null)
				{
					this.mEffects[i].Dispose();
					this.mEffects[i] = null;
				}
			}
			this.mEffects.Clear();
			this.mEffects.Add(new WaterEffect1());
			this.mEffects.Add(new WillOWisp());
			this.mEffects.Add(new BallWake());
			this.mEffects.Add(new Fog());
			this.mEffects.Add(new WaterShader1());
			this.mEffects.Add(new LavaShader());
		}

		public Effect GetEffect(string fx_name, string level_id, Level copy_effects_from)
		{
			int i = 0;
			while (i < this.mEffects.Count)
			{
				Effect effect = this.mEffects[i];
				if (Common.StrEquals(effect.GetName(), fx_name, true))
				{
					Effect effect2 = null;
					if (copy_effects_from != null)
					{
						for (int j = 0; j < copy_effects_from.mEffects.Count; j++)
						{
							if (Common.StrEquals(copy_effects_from.mEffects[j].GetName(), fx_name, true))
							{
								effect2 = copy_effects_from.mEffects[j];
								break;
							}
						}
					}
					if (effect2 != null)
					{
						return effect2;
					}
					effect.Reset(level_id);
					return effect;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		private Effect GetEffect(string fx_name, string level_id)
		{
			return this.GetEffect(fx_name, level_id);
		}

		private void CopyFrom(EffectManager m)
		{
			this.Reset();
			for (int i = 0; i < m.mEffects.Count; i++)
			{
				this.mEffects[i].CopyFrom(m.mEffects[i]);
			}
		}

		protected List<Effect> mEffects = new List<Effect>();
	}
}
