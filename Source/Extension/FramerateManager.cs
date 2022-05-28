namespace MucciArena.Extension
{
    // Credit: https://stackoverflow.com/a/44689035
    public class FramerateManager
    {
        private int samples;
        private int currentFrame;
        private double[] frametimes;
        private double currentFrametimes;

        public double Framerate
        {
            get
            {
                return samples / currentFrametimes;
            }
        }

        public FramerateManager(int Samples)
        {
            samples = Samples;
            currentFrame = 0;
            frametimes = new double[samples];
        }

        public void Update(double timeSinceLastFrame)
        {
            currentFrame++;
            if (currentFrame >= frametimes.Length) { currentFrame = 0; }

            currentFrametimes -= frametimes[currentFrame];
            frametimes[currentFrame] = timeSinceLastFrame;
            currentFrametimes += frametimes[currentFrame];
        }
    }
}
