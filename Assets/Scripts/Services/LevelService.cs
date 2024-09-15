using System;
using System.Collections.Generic;
using Arkanoid.Game;
using Arkanoid.Utility;
using UnityEngine;

namespace Arkanoid.Services
{
    public class LevelService : SingletonMonoBehaviour<LevelService>
    {
        #region Variables

        private readonly List<Block> _blocks = new();

        #endregion

        #region Events

        public event Action OnAllBlocksDestroyed;

        #endregion

        #region Unity lifecycle
        
        protected override void Awake() //  subscribe
        {
            base.Awake();
            Block.OnCreated += BlockCreatedCallback;
            Block.OnDestroyed += BlockDestroyedCallback;
        }

        private void OnDestroy() // unsubscribe
        {
            Block.OnCreated -= BlockCreatedCallback;
            Block.OnDestroyed -= BlockDestroyedCallback;
        }

        #endregion

        #region Private methods

        private void BlockCreatedCallback(Block block)
        {
            Debug.Log($"BlockCreatedCallback '{block.name}'");
            _blocks.Add(block);
        }

        private void BlockDestroyedCallback(Block block)
        {
            Debug.Log($"BlockDestroyedCallback '{block.name}'");
            _blocks.Remove(block);
            if (_blocks.Count == 0)
            {
                Debug.Log("Game Win!");
                OnAllBlocksDestroyed?.Invoke();
            }
        }

        #endregion
    }
}