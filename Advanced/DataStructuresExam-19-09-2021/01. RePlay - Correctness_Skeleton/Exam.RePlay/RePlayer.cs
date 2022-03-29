using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.RePlay
{
    public class RePlayer : IRePlayer
    {
        private Queue<string> playQueue = new Queue<string>();
        private Dictionary<string, Track> trackDict = new Dictionary<string, Track>();
        private SortedDictionary<string, List<Track>> albumsTracks = new SortedDictionary<string, List<Track>>();

        public int Count { get; private set; }

        public void AddToQueue(string trackName, string albumName)
        {
            this.ChekcNoTrackOrAlbum(trackName, albumName);
            playQueue.Enqueue(trackName);
        }

        public void AddTrack(Track track, string album)
        {
            if (!albumsTracks.ContainsKey(album))
            {
                albumsTracks.Add(album, new List<Track> { track });
                this.trackDict.Add(track.Id, track);
                this.Count++;
            }
            else if (!trackDict.ContainsKey(track.Id))
            {
                albumsTracks[album].Add(track);
                this.trackDict.Add(track.Id, track);
                this.Count++;
            }
        }

        public bool Contains(Track track)
        {
            if (trackDict.ContainsKey(track.Id))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Track> GetAlbum(string albumName)
        {
            if (!albumsTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            var tracks = albumsTracks[albumName];
            tracks.OrderBy(x => x.Plays);
            return tracks;
        }

        public Dictionary<string, List<Track>> GetDiscography(string artistName)
        {
            var result = albumsTracks.Where(kvp => kvp.Value.Equals(artistName)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return result;
        }

        public Track GetTrack(string title, string albumName)
        {
            if (!trackDict.ContainsKey(title) || !albumsTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            return trackDict[title];
        }

        public IEnumerable<Track> GetTracksInDurationRangeOrderedByDurationThenByPlaysDescending(int lowerBound, int upperBound)
        {
            var tracks = trackDict.Values.Where(x => lowerBound <= x.DurationInSeconds && x.DurationInSeconds >= upperBound);

            return tracks.OrderBy(x => x.DurationInSeconds)
                          .ThenByDescending(x => x.Plays);
        }

        public IEnumerable<Track> GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending()
        {
            var result = new List<Track>();

            var albums = albumsTracks.Reverse();
            foreach (var item in albums)
            {
                item.Value.OrderByDescending(x => x.Plays)
                          .ThenByDescending(x => x.DurationInSeconds);

                foreach (var track in item.Value)
                {
                    result.Add(track);
                }
            }

            result.Reverse();
            return result;
        }

        public Track Play()
        {
            if (playQueue.Count == 0)
            {
                throw new ArgumentException();
            }

            var trackId = playQueue.Dequeue();
            var trackToPlay = trackDict[trackId];
            trackToPlay.Plays++;

            return trackToPlay;
        }

        public void RemoveTrack(string trackTitle, string albumName)
        {
            this.ChekcNoTrackOrAlbum(trackTitle, albumName);

            var trackToRemove = trackDict[trackTitle];
            albumsTracks[albumName].Remove(trackToRemove);
            trackDict.Remove(trackTitle);

            playQueue = new Queue<string>(playQueue.Where(x => x != trackTitle));
            this.Count--;
        }

        public void ChekcNoTrackOrAlbum(string trackName, string albumName)
        {
            if (!trackDict.ContainsKey(trackName) || !albumsTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            else if (!albumsTracks[albumName].Contains(trackDict[trackName]))
            {
                throw new ArgumentException();
            }
        }
    }       
}
