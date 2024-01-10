using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TonemapURP : ScriptableRendererFeature
{
    [Header("ToneMapping")]
    public float PostExposure = 1.2f;
    public float Contrast = 1.05f;
    [Range(0, 1)]
    public float Disaturate = 0.2f;
    [Range(-1, 1)]
    public float Min = -0.05f;
    [Range(0.5f, 1.0f)]
    public float Max = 0.9f;
    [Range(0, 10)]
    public float Saturation = 0.9f;
    [Space(15)]
    [Header("Flares")]
    public bool Flares;
    [Range(0, 3)]
    public float BlurAmount = 1;

    [Range(1, 64)]
    public int QualityFlares = 16;
    // public float BlurDistance = 200;
    [Range(0.5f, 2)]
    public float FlaresRange = 0.93f;

    [Range(0, 100)]
    public int FlareOffsetCount = 30;

    [Range(0, 20)]
    public float FlareIntensity = 2;


    [Space(40)]

    public bool OnLayer2Flares = false;
    [Header("Flares Layer 2")]
    [Range(0, 3)]
    public float BlurAmount2 = 1;


    // public float BlurDistance = 200;


    [Range(0, 100)]
    public int FlareOffsetCount2 = 30;

    [Range(0, 20)]
    public float FlareIntensity2 = 2;

    public float Ylevel = 0;

    [Space(10)]
    [Header("Vignette")]
    [Range(0, 2)]
    public float VignetteIntensity = 0.5f;


    //    public Transform Point1;

    //   public Transform Point2;

    //   public Transform Point3;

  //  public Material material;

    // public bool InfoLine;
    int boolInt;
    //  public Camera Camera;





    public RenderTexture BlumTex;

    //   public ComputeShader _ComputeShader;

    public ComputeShader _ComputeShaderCleaning;

    public RenderTexture ScreenRender;

    public RenderTexture ScreenRender2;

    int Width;
    int Hight;
    int Depth;


    public bool on;

    public Shader shader;
    public Material Material_;



    class RenderPass : ScriptableRenderPass
    {



      public  TonemapURP tonemapURP;
        private Material material;
        static readonly int tempCopyString = Shader.PropertyToID("_TempCopy");
        private RenderTargetIdentifier tempCopy = new RenderTargetIdentifier(tempCopyString);
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;
        RenderTextureDescriptor opaqueDesc, half, quarter, eighths, sixths;


        static readonly int BlumTexString = Shader.PropertyToID("_BlurTex");

        static readonly int blurTempString = Shader.PropertyToID("_BlurTemp");
        static readonly int bloomTexString = Shader.PropertyToID("_BloomTex");
        static readonly int Temp1String = Shader.PropertyToID("Temp1");


        private RenderTargetIdentifier blurTemp = new RenderTargetIdentifier(blurTempString);
        private RenderTargetIdentifier BlumTex = new RenderTargetIdentifier(BlumTexString);
        private RenderTargetIdentifier Temp1 = new RenderTargetIdentifier(Temp1String);
        private RenderTargetIdentifier blurTex = new RenderTargetIdentifier(BlumTexString);

        private RenderTargetIdentifier BloomTex = new RenderTargetIdentifier(bloomTexString);

        RenderTexture Source;

        public RenderPass(Material material) : base()
        {




            //    if (intensity == 0)
            //     {
            //     Graphics.Blit(source, destination);
            //     return;
            //}



     


            //   Matrix4x4 viewToWorld = Cam.cameraToWorldMatrix;
            //   material.SetMatrix("_viewToWorld", viewToWorld);

            //   boolInt = InfoLine ? 1 : 0;

            //    material.SetInt("InfoLine", boolInt);
            //      material.SetVector("Vector1", Point1.position);
            //      material.SetVector("Vector2", Point2.position);
            //    material.SetVector("Vector3", Point3.position);

            this.material = material;




            tempTexture.Init("_TempDesaturateTexture");
        }

        void Enabled()
        {
            tonemapURP.on = true;
           
        }

        public void SetSource(RenderTargetIdentifier source)
        {
            this.source = source;
            
            
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            Camera cam = renderingData.cameraData.camera;



            CommandBuffer cmd = CommandBufferPool.Get("SimpleDesaturateFeature");
            opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            RenderTextureDescriptor cameraTextureDesc = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDesc.depthBufferBits = 0;
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDesc, FilterMode.Bilinear);



            cmd.GetTemporaryRT(tempCopyString, opaqueDesc, FilterMode.Bilinear);
            cmd.Blit(source, tempCopy);

         
            var p = GL.GetGPUProjectionMatrix(cam.projectionMatrix, true);


            p[2, 3] = p[3, 2] = 0.0f;
            p[3, 3] = 1.0f;
            var clipToWorld = Matrix4x4.Inverse(p * cam.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0, 0, -p[2, 2]), Quaternion.identity, Vector3.one);
            material.SetMatrix("clipToWorld", clipToWorld);


            material.SetFloat("PostExposure", tonemapURP.PostExposure);
            material.SetFloat("Contrast", tonemapURP.Contrast);

            if (tonemapURP.Flares)
            {
                material.SetInt("FlareOffsetCount", tonemapURP.FlareOffsetCount);
                material.SetFloat("FlareIntensity", tonemapURP.FlareIntensity);
            }
            else
            {
                material.SetInt("FlareOffsetCount", 0);
                material.SetFloat("FlareIntensity", 0);

                material.SetInt("FlareOffsetCount2", 0);
                material.SetFloat("_BlurAmount2", 0);
                material.SetFloat("FlareIntensity2", 0);
            }

            material.SetFloat("_Disaturate", tonemapURP.Disaturate);
            material.SetFloat("Saturation", tonemapURP.Saturation);
            material.SetFloat("_Min", tonemapURP.Min);
            material.SetFloat("_Max", tonemapURP.Max);
            //  material.SetFloat("BlurDistance", BlurDistance); 
            material.SetFloat("BlurRange", tonemapURP.FlaresRange);
            material.SetInt("FepthOfField", System.Convert.ToInt32(tonemapURP.Flares));
            material.SetFloat("VignetteIntensity", tonemapURP.VignetteIntensity);
            material.SetFloat("_BlurAmount", tonemapURP.BlurAmount);
            material.SetFloat("Ylevel", tonemapURP.Ylevel);
            material.SetFloat("QualityFlares", tonemapURP.QualityFlares);

            if (tonemapURP.OnLayer2Flares)
            {
                material.SetInt("FlareOffsetCount2", tonemapURP.FlareOffsetCount2);
                material.SetFloat("_BlurAmount2", tonemapURP.BlurAmount2);
                material.SetFloat("FlareIntensity2", tonemapURP.FlareIntensity2);
            }
            else
            {
                material.SetInt("FlareOffsetCount2", 0);
                material.SetFloat("_BlurAmount2", 0);
                material.SetFloat("FlareIntensity2", 0);

            }


            material.SetVector("PixelSize", new Vector4(Screen.width, Screen.height, 1, 1));


            //   cmd.GetTemporaryRT(BlumTexString, Screen.width / tonemapURP.QualityFlares, Screen.height / tonemapURP.QualityFlares, 0, FilterMode.Bilinear);

            if (tonemapURP.on || Source == null) { 

            Source = new RenderTexture(Screen.width, Screen.height, 0);
            Source.enableRandomWrite = true;
            Source.Create();
            tonemapURP.on = false;

            
        }

            cmd.Blit(source, Source);

            /*
            if (tonemapURP.on)
            {

                if (null != tonemapURP.ScreenRender)
                {
                    tonemapURP.ScreenRender.Release();
                }

                if (null != tonemapURP.ScreenRender2)
                {
                    tonemapURP.ScreenRender2.Release();
                } */

            if (tonemapURP.on || null == tonemapURP.ScreenRender || Source.width / tonemapURP.QualityFlares != tonemapURP.ScreenRender.width / tonemapURP.QualityFlares
|| Source.height / tonemapURP.QualityFlares != tonemapURP.ScreenRender.height / tonemapURP.QualityFlares)
            {
                if (null != tonemapURP.ScreenRender)
                {
                    tonemapURP.ScreenRender.Release();
                }
                material.SetInt("FepthOfField", System.Convert.ToInt32(tonemapURP.Flares));
                tonemapURP.ScreenRender = new RenderTexture(Screen.width / tonemapURP.QualityFlares, Screen.height / tonemapURP.QualityFlares, 0);
                tonemapURP.ScreenRender.enableRandomWrite = true;
                tonemapURP.ScreenRender.Create();
                tonemapURP.on = false;
                tonemapURP._ComputeShaderCleaning.SetTexture(0, "ScreenRender", tonemapURP.ScreenRender);
            }


            if (tonemapURP.on || null == tonemapURP.ScreenRender2 || Source.width / tonemapURP.QualityFlares != tonemapURP.ScreenRender2.width / tonemapURP.QualityFlares
|| Source.height / tonemapURP.QualityFlares != tonemapURP.ScreenRender2.height / tonemapURP.QualityFlares)
            {
                if (null != tonemapURP.ScreenRender2)
                {
                    tonemapURP.ScreenRender2.Release();
                }

                tonemapURP.ScreenRender2 = new RenderTexture(Screen.width / tonemapURP.QualityFlares, Screen.height / tonemapURP.QualityFlares, 0);
                tonemapURP.ScreenRender2.enableRandomWrite = true;
                tonemapURP.ScreenRender2.Create();

                tonemapURP._ComputeShaderCleaning.SetTexture(0, "ScreenRender2", tonemapURP.ScreenRender2);
            }



            

            
            RenderTexture.ReleaseTemporary(tonemapURP.BlumTex);

            //cmd.Blit(source, BlumTex, material, 1);
            //  RenderTexture.ReleaseTemporary(temp1);

            //   cmd.SetGlobalTexture(BlumTexString, BlumTex);

            tonemapURP.BlumTex = null;

            

            



            tonemapURP.BlumTex = RenderTexture.GetTemporary(Screen.width / tonemapURP.QualityFlares, Screen.height / tonemapURP.QualityFlares, 0, Source.format);


            //      var temp1 = RenderTexture.GetTemporary(Screen.width / QualityFlares, Screen.height / QualityFlares, 0, source.format);
              Graphics.Blit(Source, tonemapURP.BlumTex, material, 1);


            //   cmd.Blit(source, BlumTex, material, 1); ===============

            //   tonemapURP._ComputeShader.SetInt("FlareOffsetCount", tonemapURP.FlareOffsetCount);
            //  tonemapURP._ComputeShader.SetFloat("FlareIntensity", tonemapURP.FlareIntensity);
            //  tonemapURP._ComputeShader.SetVector("uvSize", new Vector2(Screen.width, Screen.height));
            //  tonemapURP._ComputeShader.SetFloat("_BlurAmount", tonemapURP.BlurAmount);








            //  cmd.Blit(BlumTexString, tonemapURP.BlumTex);


            //  tonemapURP._ComputeShader.SetTexture(0, "Source", Source);

            //  tonemapURP._ComputeShader.SetTexture(0, "PointsTex", tonemapURP.BlumTex);

            //  tonemapURP._ComputeShader.SetTexture(0, "ScreenRender", tonemapURP.ScreenRender);




            tonemapURP._ComputeShaderCleaning.SetInt("FlareOffsetCount", tonemapURP.FlareOffsetCount);
                tonemapURP._ComputeShaderCleaning.SetFloat("FlareIntensity", tonemapURP.FlareIntensity);
                tonemapURP._ComputeShaderCleaning.SetVector("uvSize", new Vector2(Source.width, Source.height));
                tonemapURP._ComputeShaderCleaning.SetFloat("_BlurAmount", tonemapURP.BlurAmount);

                tonemapURP._ComputeShaderCleaning.SetTexture(0, "Source", Source);

                tonemapURP._ComputeShaderCleaning.SetTexture(0, "PointsTex", tonemapURP.BlumTex);

                tonemapURP._ComputeShaderCleaning.SetTexture(0, "ScreenRender", tonemapURP.ScreenRender);

                tonemapURP._ComputeShaderCleaning.SetTexture(0, "ScreenRender2", tonemapURP.ScreenRender2);

        //   Debug.Log(Source.width);

                tonemapURP._ComputeShaderCleaning.Dispatch(0, tonemapURP.BlumTex.width / 8, tonemapURP.BlumTex.height / 8, 1);



            //    tonemapURP._ComputeShader.Dispatch(0, Source.width / 8, Source.height / 8, 1);


            material.SetTexture("_BlurTex", tonemapURP.ScreenRender);

                material.SetInt("ScreenSizeY", Screen.height);
                material.SetInt("ScreenSizeX", Screen.width);

                material.SetTexture("_BlurTex2", tonemapURP.ScreenRender2);


            
            //       }




            Blit(cmd, source, tempTexture.Identifier(), material, 0);
            Blit(cmd, tempTexture.Identifier(), source);

          //  cmd.Blit(tempCopy, source, material, 2);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
           
        }
    }

    private RenderPass renderPass;

    public override void Create()
    {
        on = true;
        var material = new Material(shader);



        this.renderPass = new RenderPass(Material_);
        renderPass.tonemapURP = this;




        renderPass.renderPassEvent = RenderPassEvent.AfterRendering;
       

    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderPass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(renderPass);
    }
}

