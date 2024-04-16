using UnityEngine.Playables;


namespace Pangoo.Common
{

    public static class PlayableDirectorExtension
    {

        public static void UpdateManuel(this PlayableDirector playableDirector)
        {
            if (playableDirector.time <= 0)
            {
                playableDirector.time = 0;
            }

            if (playableDirector.state == PlayState.Playing)
            {
                playableDirector.Evaluate();
            }

            switch (playableDirector.extrapolationMode)
            {
                case DirectorWrapMode.None:
                    if (playableDirector.time > playableDirector.duration)
                    {
                        playableDirector.Stop();
                    }
                    break;
                case DirectorWrapMode.Hold:
                    if (playableDirector.time >= playableDirector.duration)
                    {
                        playableDirector.time = playableDirector.duration;
                        playableDirector.Pause();
                    }
                    break;
                case DirectorWrapMode.Loop:
                    if (playableDirector.time > playableDirector.duration)
                    {
                        playableDirector.time = 0;
                    }
                    break;
            }
        }




    }
}
