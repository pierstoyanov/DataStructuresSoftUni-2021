using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.RePlay
{
    public class RePlayer : IRePlayer
    {
        private Dictionary<string, Track> IdTrack = new Dictionary<string, Track>();
        private SortedDictionary<string, string> TitleId = new SortedDictionary<string, string>();
        private Dictionary<string, List<string>> AlbumTracks = new Dictionary<string, List<string>> ();

        private SortedSet<(int, string)> durationTitle = new SortedSet<(int, string)> ();

        private LinkedList<string> playlist = new LinkedList<string>();

        public int Count => IdTrack.Count;

        public void AddToQueue(string trackName, string albumName)
        {
            CheckAlbumContainsTitle(trackName, albumName);

            playlist.AddLast(trackName);
        }

        public void AddTrack(Track track, string album)
        {
            
            if (!AlbumTracks.ContainsKey(album))
            {
                AlbumTracks.Add(album, new List<string> { track.Title });
            }
            else if (!AlbumTracks[album].Contains(track.Title))
            {
                AlbumTracks[album].Add(track.Title);
            }
            else
            {
                throw new ArgumentException();
            }

            IdTrack.Add(track.Id, track);
            TitleId.Add(track.Title, track.Id);

            durationTitle.Add((track.DurationInSeconds, track.Title));
        }

        public bool Contains(Track track)
        {
            return IdTrack.ContainsKey(track.Id);
        }

        public IEnumerable<Track> GetAlbum(string albumName)
        {
            if (!AlbumTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            var result = new List<Track>();

            foreach (var track in AlbumTracks[albumName])
            {
                result.Add(IdTrack[TitleId[track]]);
            }

            return result.OrderByDescending(x => x.Plays);
        }

        public Dictionary<string, List<Track>> GetDiscography(string artistName)
        {
            var result = new Dictionary<string, List<Track>>();

            foreach (var album in AlbumTracks)
            {
                foreach (var track in album.Value)
                {
                    if (IdTrack[TitleId[track]].Artist == artistName)
                    {
                        if (!result.ContainsKey(album.Key))
                        {
                            result.Add(album.Key, new List<Track> { IdTrack[TitleId[track]] });
                        }
                        else
                        {
                            result[album.Key].Add(IdTrack[TitleId[track]]);
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public Track GetTrack(string title, string albumName)
        {
            //CheckAlbumContainsTitle(title, albumName);
            CheckAlbumTitleExists(title, albumName);
            return IdTrack[TitleId[title]];
        }

        public IEnumerable<Track> GetTracksInDurationRangeOrderedByDurationThenByPlaysDescending(int lowerBound, int upperBound)
        {
            var result = new List<Track>();

            var tracks = IdTrack.Values
                .Where(x => lowerBound <= x.DurationInSeconds && x.DurationInSeconds >= upperBound)
                .OrderBy(x => x.DurationInSeconds)
                .ThenByDescending(x => x.Plays)
                .ToList();

            return result;
        }

        public IEnumerable<Track> GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending()
        {
            if (Count == 0)
            {
                return new List<Track>();
            }

            var result = new List<Track>();

            foreach (var album in AlbumTracks)
            {
                var temp = new List<Track>();

                foreach (var track in album.Value)
                {
                    temp.Add(IdTrack[TitleId[track]]);
                }

                temp
                    .OrderByDescending(x => x.Plays)
                    .ThenByDescending(x => x.DurationInSeconds);

                result.AddRange(temp);
            }

            result.Reverse();
            return result;
        }

        public Track Play()
        {
            if (playlist.Count == 0)
            {
                throw new ArgumentException();
            }

            var temp = playlist.First.ToString();
            playlist.RemoveFirst();

            IdTrack[TitleId[temp]].Plays++;

            return IdTrack[TitleId[temp]];
        }

        public void RemoveTrack(string trackTitle, string albumName)
        {
            //CheckAlbumContainsTitle(trackTitle, albumName);
            CheckAlbumTitleExists(trackTitle, albumName);
            durationTitle.Remove((IdTrack[TitleId[trackTitle]].DurationInSeconds, trackTitle));
            IdTrack.Remove(TitleId[trackTitle]);
            AlbumTracks[albumName].Remove(trackTitle);
            TitleId.Remove(trackTitle);

            playlist.Remove(trackTitle);

        }


        private void CheckAlbumContainsTitle(string trackTitle, string albumName)
        {
            if (!AlbumTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            else if (!AlbumTracks[albumName].Contains(trackTitle))
            {
                throw new ArgumentException();
            }
        }

        private void CheckAlbumTitleExists(string trackTitle, string albumName)
        {
            if (!AlbumTracks.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            if (!TitleId.ContainsKey(trackTitle))
            {
                throw new ArgumentException();
            }
        }
    }
}
