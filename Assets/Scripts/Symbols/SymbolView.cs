using System;
using UnityEngine;

namespace Symbols
{
    public class SymbolView : MonoBehaviour
    {
        public string Name { get; private set; }
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _index;

        public void Initialize(string symbolName, Sprite sprite)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            Name = symbolName;
            _spriteRenderer.sprite = sprite;
        }
        
        public void Initialize(string symbolName, Sprite sprite, int index)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            Name = symbolName;
            _spriteRenderer.sprite = sprite;
            _index = index;
        }

        public void MoveDown(float speed)
        {
            transform.localPosition -= Vector3.up * speed * Time.deltaTime;
        }
    }
}