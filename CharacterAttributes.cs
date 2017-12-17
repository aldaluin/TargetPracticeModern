using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using System.Configuration;

namespace Goldraven.AI {
 
public class CharacterAttributes : MonoBehaviour {

		public float speed { get; set; } // slow, fast
		public float hand {get; set; } // shaky, steady
		public float stamina {get; set; } // weak, strong
		public float health {get; set; } // sickly, healthy
		public float vision {get; set; } // blurry, sharp
		public float dexterity {get; set; } // clumsy, graceful
		public float levitation {get; set; } // none, glide, fly
		public float maxspeed { get; set; } // slow, fast
		public float maxhand {get; set; } // shaky, steady
		public float maxstamina {get; set; } // weak, strong
		public float maxhealth {get; set; } // sickly, healthy
		public float maxvision {get; set; } // blurry, sharp
		public float maxdexterity {get; set; } // clumsy, graceful
		public float maxlevitation {get; set; } // none, glide, fly
		[SerializeField] private int speedRoll { get; set; } // slow, fast
		[SerializeField] private int handRoll {get; set; } // shaky, steady
		[SerializeField] private int staminaRoll {get; set; } // weak, strong
		[SerializeField] private int healthRoll {get; set; } // sickly, healthy
		[SerializeField] private int visionRoll {get; set; } // blurry, sharp
		[SerializeField] private int dexterityRoll {get; set; } // clumsy, graceful
		[SerializeField] private int levitationRoll {get; set; } // none, glide, fly



		public const float BASE_WALK_SPEED = 2f;
		public const float BASE_HAND = 1f;
		public const float BASE_STAMINA = 200f;
		public const float BASE_HEALTH = 100f;
		public const float BASE_VISON = 20f;
		public const float BASE_DEXTERITY = 1f;
		public const float BASE_LEVITATION = 1f;


	// Attribute bonus getters

		private float calcBase(int ability, float average) { 
			// do not use for levitation
			switch (ability) {
			case 1:
				return average / 3f;
				break;
			case 2:
				return average / 1.5f;
				break;
			case 3:
				return average;
				break;
			case 4:
				return average * 1.5f;
				break;
			case 5:
				return average * 3f;
				break;
			case 6:
				return average * 5f;
				break;
			default:
				return average;
			}

		}


	// Use this for initialization
	void Start ()
	{
			maxspeed = calcBase (speedRoll, BASE_WALK_SPEED);
			speed = maxspeed;
			maxhand = calcBase (handRoll, BASE_HAND);
			hand = maxhand;
			maxstamina = calcBase (staminaRoll, BASE_STAMINA);
			stamina = maxstamina;
			maxhealth = calcBase (healthRoll, BASE_HEALTH);
			health = maxhealth;
			maxvision = calcBase (visionRoll, BASE_VISON);
			vision = maxvision;
			maxdexterity = calcBase (dexterityRoll, BASE_DEXTERITY);
			dexterity = maxdexterity;
			maxlevitation = 0f;
			levitation = maxlevitation;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

}
