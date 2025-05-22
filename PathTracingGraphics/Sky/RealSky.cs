using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics;
using Vectors.Vec3;

namespace PathTracingGraphics {
    public class RealSky : ISky {
        public Vec3f SunDirection { get; set; } = new Vec3f(0, 1, 0);
        public float EarthRadius { get; set; } = 6360E3F;
        public float AtmosphereRadius { get; set; } = 6420E3F;
        public float Hr { get; set; } = 7994;
        public float Hm { get; set; } = 1200;

        private static Vec3f betaR = new Vec3f(3.8e-6f, 13.5e-6f, 33.1e-6f);
        private static Vec3f betaM = 21e-6f;

        public Vec3f GetColor(Vec3f direction) {
            return ComputeIncidentLight(0, direction, 0, 10);
        }

        public Vec3f ComputeIncidentLight(Vec3f origin, Vec3f dir, float tMin, float tMax) {
            float t0, t1;
            if (!RaySphereIntersect(origin, dir, AtmosphereRadius, out t0, out t1) || t1 < 0) return 0;

            if (t0 > tMin && t0 > 0) tMin = t0;
            if (t1 < tMax) tMax = t1;
            uint numSamples = 16;
            uint numSamplesLight = 8;

            float segmentLength = (tMax - tMin) / numSamples;
            float tCurrent = tMin;
            Vec3f sumR = 0, sumM = 0; // mie and rayleigh contribution 
            float opticalDepthR = 0, opticalDepthM = 0;
            float mu = dir.Dot(SunDirection); // mu in the paper which is the cosine of the angle between the sun direction and the ray direction 
            float phaseR = 3 / (16 * (float)Math.PI) * (1 + mu * mu);
            float g = 0.76f;
            float phaseM = 3 / (8 * (float)Math.PI) * ((1 - g * g) * (1 + mu * mu)) / ((2 + g * g) * (float)Math.Pow(1 + g * g - 2 * g * mu, 1.5f));

            for (uint i = 0; i < numSamples; ++i) {
                Vec3f samplePosition = origin + (tCurrent + segmentLength * 0.5f) * dir;
                float height = samplePosition.GetMagnitude() - EarthRadius;
                // compute optical depth for light
                float hr = (float)Math.Exp(-height / Hr) * segmentLength;
                float hm = (float)Math.Exp(-height / Hm) * segmentLength;
                opticalDepthR += hr;
                opticalDepthM += hm;
                // light optical depth
                float t0Light, t1Light;
                RaySphereIntersect(samplePosition, SunDirection, AtmosphereRadius, out t0Light, out t1Light);
                float segmentLengthLight = t1Light / numSamplesLight, tCurrentLight = 0;
                float opticalDepthLightR = 0, opticalDepthLightM = 0;
                uint j;
                for (j = 0; j < numSamplesLight; ++j) {
                    Vec3f samplePositionLight = samplePosition + (tCurrentLight + segmentLengthLight * 0.5f) * SunDirection;
                    float heightLight = samplePositionLight.GetMagnitude() - EarthRadius;
                    if (heightLight < 0) break;
                    opticalDepthLightR += (float)Math.Exp(-heightLight / Hr) * segmentLengthLight;
                    opticalDepthLightM += (float)Math.Exp(-heightLight / Hm) * segmentLengthLight;
                    tCurrentLight += segmentLengthLight;
                }
                if (j == numSamplesLight) {
                    Vec3f tau = betaR * (opticalDepthR + opticalDepthLightR) + betaM * 1.1f * (opticalDepthM + opticalDepthLightM);
                    Vec3f attenuation = new Vec3f((float)Math.Exp(-tau.X), (float)Math.Exp(-tau.Y), (float)Math.Exp(-tau.Z));
                    sumR += attenuation * hr;
                    sumM += attenuation * hm;
                }
                tCurrent += segmentLength; 
            } 

            return (sumR * betaR * phaseR + sumM * betaM * phaseM) * 20;
        }

        private bool RaySphereIntersect(Vec3f o, Vec3f d, float radius, out float t0, out float t1){ 
            float a = d.X * d.X + d.Y * d.Y + d.Z * d.Z;
            float b = 2 * (d.X * o.X + d.Y * o.Y + d.Z * o.Z);
            float c = o.X * o.X + o.Y * o.Y + o.Z * o.Z - radius * radius; 
 
            if (!SolveQuadratic(a, b, c, out t0, out t1)) return false;
            if (t0 > t1){
                float temp = t0;
                t0 = t1;
                t1 = temp;
            }

            return true; 
        }
        private bool SolveQuadratic(float a, float b, float c, out float x1, out float x2) {
            x1 = 0;
            x2 = 0;

            if (b == 0) {
                // Handle special case where the the two vector ray.dir and V are perpendicular
                // with V = ray.orig - sphere.centre
                if (a == 0) return false;
                x1 = 0; x2 = (float)Math.Sqrt(-c / a);
                return true;
            }
            float discr = b * b - 4 * a * c;

            if (discr < 0) return false;

            float q = (b < 0) ? -0.5f * (b - (float)Math.Sqrt(discr)) : -0.5f * (b + (float)Math.Sqrt(discr));
            x1 = q / a;
            x2 = c / q;

            return true;
        }
    }
}
