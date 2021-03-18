#if RETAG_DEV
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ReTagTests
    {
        [UnityTest]
        public IEnumerator HasTagTest()
        {
            var tag = ScriptableObject.CreateInstance<ReTag>();
            tag.SetTagName("TestTag");

            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();

            yield return null;

            tags.SetTag(tag);

            yield return null;

            Assert.True(go.HasTag("TestTag"));
            Assert.True(go.HasTag(tag));
            Assert.True(go.HasTag(tag.GetTag));

            Assert.True(tags.HasTag("TestTag"));
            Assert.True(tags.HasTag(tag));
            Assert.True(tags.HasTag(tag.GetTag));

            Assert.True(((MonoBehaviour)tags).HasTag("TestTag"));
            Assert.True(((MonoBehaviour)tags).HasTag(tag));
            Assert.True(((MonoBehaviour)tags).HasTag(tag.GetTag));
        }

        [UnityTest]
        public IEnumerator RemoveTagTest()
        {
            var tag = ScriptableObject.CreateInstance<ReTag>();
            tag.SetTagName("TestTag");

            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();
            yield return null;

            tags.SetTag(tag);
            yield return null;
            go.RemoveTag("TestTag");
            Assert.False(go.HasTag("TestTag"));
            yield return null;

            tags.SetTag(tag);
            yield return null;
            tags.RemoveTag("TestTag");
            Assert.False(tags.HasTag("TestTag"));
            yield return null;

            tags.SetTag(tag);
            yield return null;
            ((MonoBehaviour)tags).RemoveTag("TestTag");
            Assert.False(((MonoBehaviour)tags).HasTag("TestTag"));
        }

        [UnityTest]
        public IEnumerator SetTagTest()
        {
            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();
            yield return null;

            tags.SetTag("TestTag");
            yield return null;
            Assert.True(tags.HasTag("TestTag"));

            go.SetTag("TestTag2");
            yield return null;
            Assert.True(go.HasTag("TestTag2"));

            ((MonoBehaviour)tags).SetTag("TestTag3");
            yield return null;
            Assert.True(((MonoBehaviour)tags).HasTag("TestTag3"));
        }

        [UnityTest, Performance]
        public IEnumerator HasTag_String_PerformanceTest()
        {
            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();
            var tag = ScriptableObject.CreateInstance<ReTag>();
            tag.SetTagName("TestTag");
            tags.SetTag(tag);

            yield return null;

            using (Measure.Frames().Scope())
            {
                for (int j = 0; j < 100; j++)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        go.HasTag("TestTag");
                    }
                    yield return null;
                }
            }
        }

        [UnityTest, Performance]
        public IEnumerator HasTag_Tag_PerformanceTest()
        {
            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();
            var tag = ScriptableObject.CreateInstance<ReTag>();
            tag.SetTagName("TestTag");
            tags.SetTag(tag);

            yield return null;

            using (Measure.Frames().Scope())
            {
                for (int j = 0; j < 100; j++)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        go.HasTag(tag);
                    }
                    yield return null;
                }
            }
        }

        [UnityTest, Performance]
        public IEnumerator HasTag_TagIdentifier_PerformanceTest()
        {
            var go = new GameObject();
            var tags = go.AddComponent<ReTags>();
            var tag = ScriptableObject.CreateInstance<ReTag>();
            tag.SetTagName("TestTag");
            var tagi = tag.GetTag;
            tags.SetTag(tag);

            yield return null;

            using (Measure.Frames().Scope())
            {
                for (int j = 0; j < 100; j++)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        go.HasTag(tagi);
                    }
                    yield return null;
                }
            }
        }
    }
}
#endif