﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System.Numerics;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace SixLabors.ImageSharp.Processing.Processors
{
    internal abstract class CenteredAffineProcessor<TPixel> : AffineProcessor<TPixel>
        where TPixel : struct, IPixel<TPixel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CenteredAffineProcessor{TPixel}"/> class.
        /// </summary>
        /// <param name="matrix">The transform matrix</param>
        /// <param name="sampler">The sampler to perform the transform operation.</param>
        protected CenteredAffineProcessor(Matrix3x2 matrix, IResampler sampler)
            : base(matrix, sampler)
        {
        }

        /// <inheritdoc/>
        protected override Matrix3x2 GetProcessingMatrix(Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            var translationToTargetCenter = Matrix3x2.CreateTranslation(-destinationRectangle.Width * .5F, -destinationRectangle.Height * .5F);
            var translateToSourceCenter = Matrix3x2.CreateTranslation(sourceRectangle.Width * .5F, sourceRectangle.Height * .5F);
            return translationToTargetCenter * this.TransformMatrix * translateToSourceCenter;
        }
    }
}