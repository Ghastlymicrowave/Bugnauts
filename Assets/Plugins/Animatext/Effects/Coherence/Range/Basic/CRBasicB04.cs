﻿// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Basic/Basic - B04", fileName = "New CRBasicB04 Preset", order = 369)]
    public sealed class CRBasicB04 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startPosition = Vector2.zero;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 position = new Vector2(0, 100);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        public bool continuousEasing = true;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Range; }
        }

        protected override int unitCount
        {
            get { return 1; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            float progress = GetCurrentProgress(0);

            progress = EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.Basic(progress, continuousEasing);

            Vector2 anchorPoint = range.GetAnchorPoint(anchorType) + anchorOffset;

            range.Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
            range.Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
            range.Move(Vector2.LerpUnclamped(startPosition, position, progress));
        }
    }
}