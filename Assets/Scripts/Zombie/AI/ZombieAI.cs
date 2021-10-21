using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public struct Data
    {
        private float _zombieRange, _zombiePosX, _targetPos;

        public  bool attacked;
        // public Node _chaseTopNode;

        public void Update()
        {
            var distance = Mathf.Abs(_targetPos - _zombiePosX);
            if (distance <= _zombieRange && !attacked)
            {
                attacked =  true;
                // Debug.Log("Attack bool Attacked = " + attacked);
            }

            // _chaseTopNode.Evaluate(_chaseTopNode);
        }

        /*public void ConstructBehaviorTree()
        {
            RangeNode checkAttackRangeNode = new RangeNode(_zombie.Range,_zombie.transform, GameManager.Instance.targetPos);
        
            AttackNode attackNode = new AttackNode(_zombie);
        
            MoveNode chaseNode = new MoveNode(_zombie);

            Sequence attackSequence = new Sequence(new List<Node> {checkAttackRangeNode, attackNode});

            _chaseTopNode = new Selector(new List<Node> {attackSequence, chaseNode});
        }*/

        public Data(ZombieAI zombieAI)
        {
            _zombieRange = zombieAI.GetComponent<Zombie>().Range;
            _zombiePosX = zombieAI.transform.position.x;
            _targetPos = GameManager.Instance.targetPos.position.x;
            attacked = false;
            /*_zombie = zombieAI.GetComponent<Zombie>();
            _chaseTopNode = new Selector(new List<Node>());
            ConstructBehaviorTree();*/
        }
    }
}