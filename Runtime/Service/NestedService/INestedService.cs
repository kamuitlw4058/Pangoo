namespace Pangoo.Service
{
    public interface INestedService
    {
        int Priority { get; }

        T GetVariable<T>(string key, T default_val = default(T));

        void SetVariable<T>(string key, object val, bool overwrite = true);

        T GetService<T>() where T : class, INestedService;

        void AddService(INestedService service);

        void RemoveService(INestedService service);

        INestedService[] Childern { get; }

        INestedService Parent { get; set; }


        float DeltaTime { get; }

        void Awake();

        void Start();

        void Enable();

        void FixedUpdate();

        void Update();


        void Disable();

        void Destroy();

        void DrawGizmos();


        void DoAwake();
        void DoStart();

        void DoEnable();

        void DoFixedUpdate();
        void DoUpdate();

        void DoDisable();

        void DoDestroy();

        void DoDrawGizmos();
    }
}