/* Copyright 2017 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;

//Converts the position-delta of a transform to the pitch property on a GvrAudioSource
public class TransformVelocityToPitch : MonoBehaviour {
    [SerializeField] public Transform source = null;
    [SerializeField] public GvrAudioSource target = null;
    [SerializeField] public float baseValue = 0f;
    [SerializeField] public float maxValue = 1f;
    [SerializeField] public float velocityMultiplier = 1f;
    [SerializeField] public float lerpFactor = 1f;

    [NonSerialized] Vector3? previousPos = null;

    void OnEnable() {
    }

    void Update() {
        if(target != null && source != null) {
            Vector3 pos = source.transform.position;
            if(previousPos.HasValue) {
                float velocity = (pos - previousPos.Value).magnitude;
                float mag = Mathf.Min(baseValue + velocity * velocityMultiplier, maxValue);
                target.pitch = Mathf.Lerp(target.pitch, mag, Mathf.Clamp(lerpFactor * Time.fixedDeltaTime, 0f, 1f));
            }
            previousPos = pos;
        }
    }
}