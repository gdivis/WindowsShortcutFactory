﻿namespace WindowsShortcutFactory;

/// <summary>
/// Contains the path of an icon and optionally an index.
/// </summary>
public readonly struct IconLocation : IEquatable<IconLocation>
{
    /// <summary>
    /// The value that represents no icon.
    /// </summary>
    public static readonly IconLocation None = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="IconLocation"/> struct.
    /// </summary>
    /// <param name="path">Path of the icon file or resource file.</param>
    /// <param name="index">Index of the icon if a resource file is specified.</param>
    public IconLocation(string path, int index = 0)
    {
        this.Path = path;
        this.Index = index;
    }

    /// <summary>
    /// Converts a path to a <see cref="IconLocation"/> instance with an <see cref="Index"/> of 0.
    /// </summary>
    /// <param name="path">Path of the icon file.</param>
    public static implicit operator IconLocation(string path) => string.IsNullOrEmpty(path) ? default : new IconLocation(path);
    /// <summary>
    /// Tests for equality between two <see cref="IconLocation"/> values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>True if <paramref name="a"/> is equivalent to <paramref name="b"/>; otherwise false.</returns>
    public static bool operator ==(IconLocation a, IconLocation b) => a.Equals(b);
    /// <summary>
    /// Tests for inequality between two <see cref="IconLocation"/> values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <returns>True if <paramref name="a"/> is not equivalent to <paramref name="b"/>; otherwise false.</returns>
    public static bool operator !=(IconLocation a, IconLocation b) => !a.Equals(b);

    /// <summary>
    /// Gets the path of the icon file or resource file.
    /// </summary>
    public string Path { get; }
    /// <summary>
    /// Gets the index of the icon if <see cref="Path"/> refers to a resource file.
    /// </summary>
    public int Index { get; }
    /// <summary>
    /// Gets a value indicating whether a path is specified.
    /// </summary>
    public bool IsValid => !string.IsNullOrEmpty(this.Path);

    /// <inheritdoc/>
    public override string ToString()
    {
        if (!this.IsValid)
            return "None";

        if (this.Index <= 0)
            return this.Path;

        return $"{this.Path};{this.Index}";
    }
    /// <inheritdoc/>
    public bool Equals(IconLocation other)
    {
        if (this.IsValid && other.IsValid)
            return this.Path == other.Path && this.Index == other.Index;
        else
            return this.IsValid == other.IsValid;
    }
    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is IconLocation i && this.Equals(i);
    /// <inheritdoc/>
    public override int GetHashCode() => this.Path?.GetHashCode() ?? 0;
}
