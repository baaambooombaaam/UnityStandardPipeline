/*
This script is synchronize the targe particles postion with the source position.
*/
using UnityEngine;
namespace InnoGames.Game.Fx
{
	
	public class FxSyncParticleModules : MonoBehaviour
	{
		public GameObject SourcePSystem;
		public GameObject[] TargetPSystem;
		public int EmissionRate = 0;		
							
		void Start() 
		{
			//Set Emission on all Particle Systems
			var sourceSys = SourcePSystem.GetComponent<ParticleSystem>();
			var sourceEmission = sourceSys.emission;
			sourceEmission.enabled = true;
			sourceEmission.rateOverTime = EmissionRate;
			
			foreach(var b in TargetPSystem)
			{
				var targetSys = b.GetComponent<ParticleSystem>();
				var targetEmission = targetSys.emission;
				targetEmission.enabled = true;
				targetEmission.rateOverTime = EmissionRate;
			}
		}
		
		void LateUpdate()
		{
			//Get particles from source system		
			var sourceSys = SourcePSystem.GetComponent<ParticleSystem>();
			var sourceParticle = new ParticleSystem.Particle[sourceSys.particleCount];
			sourceSys.GetParticles(sourceParticle);
			
			//Get each target particle system
			foreach(var b in TargetPSystem)
			{
				var targetSys = b.GetComponent<ParticleSystem>();
				var targetParticle = new ParticleSystem.Particle[targetSys.particleCount];
				targetSys.GetParticles(targetParticle);
				
				//Get each particles of each target particle system 
				for(int targetId = 0; targetId < targetParticle.Length; targetId++)
				{
					//Get each particles of each source particle system 
					for(int sourceId = 0; sourceId < sourceParticle.Length; sourceId++)
					{
						//Set new source pos for each target particles 
						if (targetId == sourceId) 
						{
							targetParticle[targetId].position = sourceParticle[sourceId].position;
						}
					}
				}
				targetSys.SetParticles(targetParticle, targetSys.particleCount);
			}
		}
	}
}