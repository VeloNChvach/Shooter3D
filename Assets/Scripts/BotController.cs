using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter3D.Controller
{
    public class BotController : MonoBehaviour
    {
        private List<Bot> _botList = new List<Bot>();

        private void Start()
        {
            Init();
        }

        public List<Bot> GetBotList
        {
            get { return _botList; }
            private set { _botList = value; }
        }

        public void Init()
        {
            Transform tempTarget = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            Bot[] tempBots = Bot.FindObjectsOfType<Bot>() as Bot[];

            foreach (var tempBot in tempBots)
                AddBotToList(tempBot);

            int i = -1;
            foreach (var tempBot in GetBotList)
            {
                tempBot._agent.avoidancePriority = ++i;
                tempBot.Target = tempTarget;
            }
        }

        public void AddBotToList(Bot bot)
        {
            if (!GetBotList.Contains(bot))
                GetBotList.Add(bot);
        }

        public void RemoveBotToList(Bot bot)
        {
            if (GetBotList.Contains(bot))
                GetBotList.Remove(bot);
        }
    }
}