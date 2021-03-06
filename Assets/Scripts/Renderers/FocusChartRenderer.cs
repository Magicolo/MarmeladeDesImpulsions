﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class FocusChartRenderer : PMonoBehaviour
{
	[SerializeField, PropertyField]
	Mesh mesh;
	[SerializeField, PropertyField(typeof(MinAttribute), 1)]
	int slices = 2;
	[SerializeField, PropertyField(typeof(MinAttribute), 0)]
	float fillRatio = 0.5f;


	public Mesh Mesh
	{
		get { return mesh; }
		set
		{
			mesh = value;
			UpdatePolygon();
		}
	}
	public int Slices
	{
		get { return slices; }
		set
		{
			slices = value;
			UpdatePolygon();
		}
	}
	public float FillRatio
	{
		get { return fillRatio; }
		set
		{
			fillRatio = value;
			UpdatePolygon();
		}
	}

	readonly Lazy<MeshFilter> cachedMeshFilter;
	public MeshFilter MeshFilter { get { return cachedMeshFilter.Value; } }

	public FocusChartRenderer()
	{
		cachedMeshFilter = new Lazy<MeshFilter>(GetComponent<MeshFilter>);
	}

	void UpdatePolygon()
	{
		MeshFilter.sharedMesh = mesh;

		if (mesh == null)
			return;

		mesh.Clear();
		var vertices = new Vector3[2 * Slices + 1];
		var triangles = new int[Slices * 3];

		for (int i = 0; i < Slices; i++)
		{
			int verticesIndex = i * 2;
			float halfAngle = (360f / Slices * fillRatio) / 2;
			vertices[verticesIndex] = Vector2.up.Rotate(i * (360f / Slices) - halfAngle);
			vertices[verticesIndex + 1] = Vector2.up.Rotate(i * (360f / Slices) + halfAngle);
			triangles[i * 3] = Slices * 2;
			triangles[i * 3 + 1] = verticesIndex;
			triangles[i * 3 + 2] = (verticesIndex + 1);
		}

		mesh.vertices = vertices;
		mesh.uv = vertices.Convert(v => v.ToVector2());
		mesh.triangles = triangles;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();
	}

	protected override void OnValidate()
	{
		base.OnValidate();

		UpdatePolygon();
	}

	void Reset()
	{
		UpdatePolygon();
	}
}
