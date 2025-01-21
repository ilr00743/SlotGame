using System;
using UnityEngine;

namespace Symbols
{
    public class SymbolView : MonoBehaviour
    {
        private string _name;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _index;

        public void Initialize(string symbolName, Sprite sprite)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _name = symbolName;
            _spriteRenderer.sprite = sprite;
        }
        
        public void Initialize(string symbolName, Sprite sprite, int index)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _name = symbolName;
            _spriteRenderer.sprite = sprite;
            _index = index;
        }

        public void MoveDown(float speed)
        {
            transform.localPosition -= Vector3.up * speed * Time.deltaTime;
        }
    }
}