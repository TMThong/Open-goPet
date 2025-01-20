using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.Collections
{
    public class TimeTracker<T>
    {
        private Mutex mutex = new Mutex();

        public ConcurrentDictionary<T, CopyOnWriteArrayList<DateTime>> Tracks { get; } = new ConcurrentDictionary<T, CopyOnWriteArrayList<DateTime>>();

        public TimeSpan Time { get; }

        public int MaxTrack { get; } = 10;


        public TimeTracker(TimeSpan time)
        {
            Time = time;
        }

        public TimeTracker(TimeSpan time, int maxTrack) : this(time)
        {
            MaxTrack = maxTrack;
        }

        public void Add(T key, DateTime time)
        {
            mutex.WaitOne();
            try
            {
                if (!Tracks.ContainsKey(key))
                {
                    Tracks.TryAdd(key, new CopyOnWriteArrayList<DateTime>());
                }
                Tracks[key].Add(time);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public void Add(T key)
        {
            Add(key, DateTime.Now);
        }

        public void CleanOldTrack()
        {
            mutex.WaitOne();
            try
            {
                foreach (var track in Tracks)
                {
                    var list = track.Value;
                    foreach (var item1 in list)
                    {
                        if (DateTime.Now - item1 > Time)
                        {
                            track.Value.Remove(item1);
                        }
                    }
                    if (track.Value.IsEmpty)
                    {
                        Tracks.TryRemove(track.Key, out _);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public int Count(T key)
        {
            mutex.WaitOne();
            try
            {
                if (Tracks.ContainsKey(key))
                {
                    return Tracks[key].Count;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public bool IsLimited(T key)
        {
            return Count(key) >= MaxTrack;
        }
    }
}
