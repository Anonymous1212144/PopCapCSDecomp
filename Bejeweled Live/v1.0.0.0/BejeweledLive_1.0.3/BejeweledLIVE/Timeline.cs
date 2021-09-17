using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class Timeline<ClientType> : AbstractTimeline
	{
		private static void deleteChannel(Timeline<ClientType>.Channel channel)
		{
			channel.Dispose();
		}

		public Timeline(ClientType client)
		{
			this.mClient = client;
		}

		public override void Dispose()
		{
			foreach (Timeline<ClientType>.Channel channel in this.mChannels)
			{
				Timeline<ClientType>.deleteChannel(channel);
			}
			base.Dispose();
		}

		public void SetEvent(int tick, Timeline<ClientType>.EventFunc func)
		{
			Timeline<ClientType>.Event @event = new Timeline<ClientType>.Event();
			@event.tick = tick;
			@event.func = func;
			this.mEvents.Add(@event);
			this.mEvents.Sort(new Comparison<Timeline<ClientType>.Event>(Timeline<ClientType>.CompareEvents));
		}

		private static int CompareEvents(Timeline<ClientType>.Event a, Timeline<ClientType>.Event b)
		{
			return a.tick.CompareTo(b.tick);
		}

		public void ReserveChannels(int count)
		{
			if (this.mChannels.Capacity < count)
			{
				this.mChannels.Capacity = count;
			}
		}

		public int AddChannel<KT>(Action<ClientType, KT> pointerToMemberData, KeyInterpolatorGeneric<KT>.InterpolatorMethod m) where KT : struct
		{
			Timeline<ClientType>.MemberDataChannel<KT> memberDataChannel = new Timeline<ClientType>.MemberDataChannel<KT>(pointerToMemberData, m);
			int count = this.mChannels.Count;
			this.mChannels.Add(memberDataChannel);
			return count;
		}

		public void SetKey<KT>(int channelId, int tick, KT value, bool ease, bool tween) where KT : struct
		{
			Timeline<ClientType>.Channel channel = this.mChannels[channelId];
			Timeline<ClientType>.TypedChannel<KT> typedChannel = channel as Timeline<ClientType>.TypedChannel<KT>;
			Debug.ASSERT(typedChannel != null);
			typedChannel.SetKey(tick, value, ease, tween);
			this.mLastTick = Math.Max(this.mLastTick, tick);
		}

		public override void FireEvents()
		{
			if (this.mEvents.Count > 0)
			{
				while (this.mEvent < this.mEvents.Count)
				{
					if (this.mElapsedTicksI < this.mEvents[this.mEvent].tick)
					{
						return;
					}
					this.mEvents[this.mEvent].func();
					this.mEvent++;
				}
			}
			else
			{
				this.mEvent = 1;
			}
		}

		public override void Apply()
		{
			foreach (Timeline<ClientType>.Channel channel in this.mChannels)
			{
				channel.Apply(ref this.mClient, this.mElapsedTicksF);
			}
		}

		public override void Clear()
		{
			base.Clear();
			this.mEvents.Clear();
			this.mEvent = -1;
			foreach (Timeline<ClientType>.Channel channel in this.mChannels)
			{
				Timeline<ClientType>.deleteChannel(channel);
			}
			this.mChannels.Clear();
		}

		public override void Start()
		{
			base.Start();
			if (this.mEvents.Count > 0)
			{
				this.mEvent = 0;
				return;
			}
			this.mEvent = -1;
		}

		public override bool IsRunning()
		{
			return base.IsRunning() && this.mEvent < this.mEvents.Count;
		}

		public override bool AtEnd()
		{
			return this.mEvent >= this.mEvents.Count;
		}

		private ClientType mClient;

		private List<Timeline<ClientType>.Event> mEvents = new List<Timeline<ClientType>.Event>();

		private int mEvent;

		private List<Timeline<ClientType>.Channel> mChannels = new List<Timeline<ClientType>.Channel>();

		public delegate void EventFunc();

		public class Event
		{
			public int CompareTo(Timeline<ClientType>.Event anEvent)
			{
				if (this.tick > anEvent.tick)
				{
					return 1;
				}
				if (this.tick < anEvent.tick)
				{
					return -1;
				}
				return 0;
			}

			public override bool Equals(object obj)
			{
				return obj is Timeline<ClientType>.Event && this.CompareTo((Timeline<ClientType>.Event)obj) == 0;
			}

			public override int GetHashCode()
			{
				return this.func.GetHashCode() * this.tick.GetHashCode();
			}

			public int tick;

			public Timeline<ClientType>.EventFunc func;
		}

		private abstract class Channel
		{
			public virtual void Dispose()
			{
			}

			public virtual void Apply(ref ClientType client, float time)
			{
			}
		}

		private abstract class TypedChannel<KT> : Timeline<ClientType>.Channel where KT : struct
		{
			protected TypedChannel(KeyInterpolatorGeneric<KT>.InterpolatorMethod m)
			{
				this.mInterpolator = new KeyInterpolatorGeneric<KT>(m);
			}

			public void SetKey(int tick, KT value, bool ease, bool tween)
			{
				this.mInterpolator.SetKey(tick, value, ease, tween);
			}

			public KeyInterpolatorGeneric<KT> mInterpolator;
		}

		private class MemberDataChannel<KT> : Timeline<ClientType>.TypedChannel<KT> where KT : struct
		{
			public MemberDataChannel(Action<ClientType, KT> pointerToMember, KeyInterpolatorGeneric<KT>.InterpolatorMethod m)
				: base(m)
			{
				this.mPointerToMemberData = pointerToMember;
			}

			public override void Apply(ref ClientType client, float time)
			{
				KT kt = this.mInterpolator.Tick(time);
				this.mPointerToMemberData.Invoke(client, kt);
			}

			private Action<ClientType, KT> mPointerToMemberData;
		}

		private class MemberFunctionChannel<KT> : Timeline<ClientType>.TypedChannel<KT> where KT : struct
		{
			public MemberFunctionChannel(Action<ClientType, KT> pointerToMember, KeyInterpolatorGeneric<KT>.InterpolatorMethod m)
				: base(m)
			{
				this.mPointerToMemberFunction = pointerToMember;
			}

			public override void Apply(ref ClientType client, float tick)
			{
				KT kt = this.mInterpolator.Tick(tick);
				this.mPointerToMemberFunction.Invoke(client, kt);
			}

			private Action<ClientType, KT> mPointerToMemberFunction;
		}
	}
}
