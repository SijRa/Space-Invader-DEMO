using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DistributionManager 
{
	/// <summary>
	/// Sample from Binomial Distribution
	/// </summary>
	/// <param name = "chance" >chance of success (1)</param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with 1 and 0 </returns>
	public static int[] BinomialSample (float chance, int sampleLenght)
	{
		int[] sample = new int[sampleLenght];
		float[] uniformSample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
			uniformSample[i] = UnityEngine.Random.Range(0f,1f);

		for(int i = 0; i < sampleLenght; i++)
			if(uniformSample[i] <= chance)
				sample[i] = 1;
			else
				sample[i] = 0;

		return sample;
	}

	/// <summary>
	/// Sample from Polinomial Distribution
	/// </summary>
	/// <param name = "chances" > Array with chance for each value </param>
	/// <param name = "values" > Array with values </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array filled with numbers from values array</returns>
	public static float[] PolinomialSample (float[] chances,float[] values,int sampleLenght)
	{
		float totalChance = 0;
		for(int i = 0; i < chances.Length; i++)
			totalChance += chances[i];
		if(totalChance == 1 && chances.Length == values.Length)
		{
			float[] sample = new float[sampleLenght];
			float[] uniformSample = new float[sampleLenght];
			for(int i = 0; i < uniformSample.Length; i++)
				uniformSample[i] = UnityEngine.Random.Range(0f,1f);

			for(int i = 0; i < uniformSample.Length; i++)
			{
				int currentValue = 0;
				float temp = chances[currentValue];
				while(temp < uniformSample[i])
				{
					currentValue++;
					temp += chances[currentValue];
				}
				sample[i] = values[currentValue];
			}

			return sample;
			
		} else
		{
			Debug.Log("ERROR!!");
			Debug.Log("Chances sum != 1 or values lenght != chances lenght");
			return null;
		}
	}

	/// <summary>
	/// Sample from Poisson Distribution
	/// </summary>
	/// <param name = "lambda" > Parameter for Poisson distribution </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with integers </returns>
	public static int[] PoissSample (float lambda, int sampleLenght)
	{
		if(lambda > 0)
		{
			int[] sample = new int[sampleLenght];
			for(int i = 0; i < sample.Length; i++)
			{
				float expLambda = Mathf.Exp(-1 * lambda);
				int sampleValue = 1;
				float p = UnityEngine.Random.Range(0f,1f);
				while(p > expLambda)
				{
					sampleValue++;
					p *= UnityEngine.Random.Range(0f,1f);
				}
				sample[i] = sampleValue;
			}
			return sample;
		} else
		{
			Debug.Log("ERROR");
			Debug.Log("Lambda < 0");
			return null;
		}
	}

	/// <summary>
	/// Sample from Cauchy Distribution
	/// </summary>
	/// <param name = "location" > Parameter for head location </param>
	/// <param name = "scale" > How "high" head is </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] CauchySample (float location, float scale, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		float[] uniformSample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
			uniformSample[i] = UnityEngine.Random.Range(0f,1f);
		if(scale > 0)
		{
			for(int i = 0; i < sampleLenght; i++)
				sample[i] = ((-1 * Mathf.Tan(Mathf.PI * uniformSample[i] - Mathf.PI/2)) * scale) + location;

			return sample;
		}
		else
		{
			Debug.Log("ERROR!!!");
			Debug.Log("Scale < 0");
			return null;
		}
	}

	/// <summary>
	/// Sample from Exponential Distribution
	/// </summary>
	/// <param name = "rate" > Rate parameter </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] ExponentialSample (float rate, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		float[] uniformSample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
			uniformSample[i] = UnityEngine.Random.Range(0f,1f);

		for(int i = 0; i < sampleLenght; i++)
		{
			sample[i] = Mathf.Log(1 - uniformSample[i]) * (-1 * rate);
		}

		return sample;
	}

	/// <summary>
	/// Sample from Normal(Gauss) Distribution
	/// </summary>
	/// <param name = "mean" > Mean value for Normal Distribution </param>
	/// <param name = "standartDeviation" > Variance(Standart Devitation squared!!) value  </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] NormalDistSample (float mean, float standartDevitation, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		for(int i = 0; i < sample.Length; i++)
		{
			float u1 = UnityEngine.Random.Range(0f,1f);
			float u2 = UnityEngine.Random.Range(0f,1f);
			sample[i] = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Sin(Mathf.PI * 2 * u2) * Mathf.Sqrt(standartDevitation) + mean;
		}

		return sample;
	}

	/// <summary>
	/// Sample from Gamma Distribution
	/// </summary>
	/// <param name = "shape" > Shape parameter </param>
	/// <param name = "scale" > Scale parameter  </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] GammaSample (float shape, float scale, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
//		float[] uniformSample = new float[sampleLenght];
		float delta = shape - (int)shape;
		int k = (int)shape;

		for(int i = 0; i < sampleLenght; i++)
		{
			float u1 = UnityEngine.Random.Range(0f,1f);
			float u2 = UnityEngine.Random.Range(0f,1f);
			float u3 = UnityEngine.Random.Range(0f,1f);
			float e = Mathf.Exp(1);
			float ksi = 0;
			float eta = 0;
			if(u1 < (e)/(e + delta))
			{
				ksi = Mathf.Pow(u2,1/delta);
				eta = u3 * Mathf.Pow(ksi,delta-1);
			}
			else
			{
				ksi = 1 - Mathf.Log(u2);
				eta = u3 * Mathf.Exp(-1 * ksi);
			}
			while ( eta > Mathf.Pow(ksi,delta-1) * Mathf.Exp(-1 * ksi))
			{
				u1 = UnityEngine.Random.Range(0f,1f);
				u2 = UnityEngine.Random.Range(0f,1f);
				u3 = UnityEngine.Random.Range(0f,1f);
				e = Mathf.Exp(1);
				ksi = 0;
				eta = 0;
				if(u1 < (e)/(e + delta))
				{
					ksi = Mathf.Pow(u2,1/delta);
					eta = u3 * Mathf.Pow(ksi,delta-1);
				}
				else
				{
					ksi = 1 - Mathf.Log(u2);
					eta = u3 * Mathf.Exp(-1 * ksi);
				}
			}

			float temp = 0;
			for(int j = 0; j < k; j++)
				temp += Mathf.Log(UnityEngine.Random.Range(0f,1f));
			sample[i] = (ksi - temp) * scale;
		}

		return sample;
	}

	/// <summary>
	/// Sample from Beta Distribution
	/// </summary>
	/// <param name = "alpha" > Alpha parameter </param>
	/// <param name = "beta" > Beta parameter  </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] BetaSample (float alpha, float beta, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
		{
			float x = GammaSample(alpha,1,1)[0];
			float y = GammaSample(beta,1,1)[0];
			sample[i] = x / (x + y);
		}

		return sample;
	}

	/// <summary>
	/// Sample from Rayleigh Distribution
	/// </summary>
	/// <param name = "scale" > Scale parameter </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] RayleighSample (float scale, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		float[] uniformSample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
			uniformSample[i] = UnityEngine.Random.Range(0f,1f);

		for(int i = 0; i < sampleLenght; i++)
			sample[i] = scale * (Mathf.Sqrt(-2 * Mathf.Log(1 - uniformSample[i])));

		return sample;
	}

	/// <summary>
	/// Sample from Bates Distribution
	/// </summary>
	/// <param name = "a" > interval left border </param>
	/// <param name = "beta" > interval right border </param>
	/// <param name = "n" > Amount of uniform distributions </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] BatesSample (float a, float b,int n, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
		{
			float sum = 0;
			for(int j = 0; j < n; j++)
				sum += UnityEngine.Random.Range(a,b);
			sum /= n;
			sample[i] = sum;
		}

		return sample;
	}

	/// <summary>
	/// Sample from Chi Square (Pirson) Distribution
	/// </summary>
	/// <param name = "freedomDegree" > Degree of Freedoms amount </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] ChiSquareSample (int freedomDegree, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];

		for(int i = 0; i < sampleLenght; i++)
		{
			float q = 0;
			for(int j = 0; j < freedomDegree; j++)
			{
				float stNorm = NormalDistSample(0,1,1)[0];
				q += stNorm*stNorm;
			}
			sample[i] = q;
		}

		return sample;
	}

	/// <summary>
	/// Sample from Fisher Distribution
	/// </summary>
	/// <param name = "firstDegreeOfFreedom" > First Degree of Freedoms amount </param>
	/// <param name = "secondDegreeOfFreedom" > Second Degree of Freedoms amount </param>
	/// <param name = "sampleLenght" > sample array lenght </param>
	/// <returns> Array with floats </returns>
	public static float[] FisherSample(int firstDegreeOfFreedom, int secondDegreeOfFreedom, int sampleLenght)
	{
		float[] sample = new float[sampleLenght];
		for(int i = 0; i < sampleLenght; i++)
		{
			float u1 = ChiSquareSample(firstDegreeOfFreedom,1)[0];
			float u2 = ChiSquareSample(secondDegreeOfFreedom,1)[0];
			sample[i] = (u1/firstDegreeOfFreedom)/(u2/secondDegreeOfFreedom);
		}
		return sample;
	}

	/// <summary>
	/// Sample mean (average value)
	/// </summary>
	/// <param name = "sample" > Sample... </param>
	/// <returns> Sample mean </returns>
	public static float SampleMean (float[] sample)
	{
		float mean = 0;
		for(int i = 0; i < sample.Length; i++)
			mean += sample[i];
		mean /= sample.Length;
		return mean;
	}

	/// <summary>
	/// Sample Standart Devitation(SQRT(Variance))
	/// </summary>
	/// <param name = "sample" > Sample... </param>
	/// <returns> Standart Deviation mean </returns>
	public static float SampleStandartDevitation (float[] sample)
	{
		float mean = SampleMean(sample);
		float stDev = 0;
		for(int i = 0; i < sample.Length; i++)
			stDev += (mean - sample[i])*(mean - sample[i]);
		stDev /= sample.Length;
		return Mathf.Sqrt(stDev);
	}

	/// <summary>
	/// Sample Median (mid number from sorted sample)
	/// </summary>
	/// <param name = "sample" > Sample... </param>
	/// <returns> Sample Median </returns>
	public static float SampleMedian (float[] sample)
	{
		List<float> sampleList = new List<float>();
		for(int i = 0; i < sample.Length; i++)
			sampleList.Add(sample[i]);
		sampleList.Sort();

		float median = 0;
		if(sampleList.Count%2 == 0)
			median = (sampleList[sampleList.Count/2 - 1] + sampleList[sampleList.Count/2])/2;
		else
			median = sampleList[(sampleList.Count- 1)/2];
		return median;
	}

	/// <summary>
	/// Sample Moda (most popular number)
	/// </summary>
	/// <param name = "sample" > Sample... </param>
	/// <returns> Sample Moda </returns>
	public static float SampleModa (float[] sample)
	{
		List<float> sampleList = new List<float>();
		List<float> values = new List<float>();
		List<float> counts = new List<float>();
		for(int i = 0; i < sample.Length; i++)
			sampleList.Add(sample[i]);
		
		while(sampleList.Count > 0)
		{
			if(!values.Contains(sampleList[0]))
			{
				values.Add(sampleList[0]);
				float count = 0;
				for(int i = 0; i < sampleList.Count; i++)
					if(sampleList[i] == sampleList[0])
						count++;
				counts.Add(count);
			} else
			{
				sampleList.RemoveAt(0);
			}
		}
		float maxCount = 0;
		int maxIndex = 0;
		for(int i = 0; i < counts.Count; i++)
			if(counts[i] > maxCount)
			{
				maxCount = counts[i];
				maxIndex = i;
			}

		return values[maxIndex];
	}
}
