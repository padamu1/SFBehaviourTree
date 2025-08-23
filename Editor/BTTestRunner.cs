using UnityEngine;
using UnityEditor;
using SimulFactory.BehaviourTree;
using SimulFactory.BehaviourTree.Helper;
using System.Collections;

namespace SimulFactory.BehaviourTree.Editor
{
    public class BTTestRunner : EditorWindow
    {
        private IBTNode behaviorTree;
        private BTContext context;
        private bool isRunning = false;
        private float lastUpdateTime;
        private Vector2 scrollPosition;
        private string logMessages = "";
        private bool autoRun = false;
        private float autoRunInterval = 0.1f;

        private bool isAlive = true;
        private bool canAttack = true;
        private float health = 100f;
        private float attackCooldown = 0f;
        private Vector3 targetPosition = Vector3.zero;
        private Vector3 currentPosition = Vector3.zero;

        [MenuItem("Window/Behaviour Tree Test Runner")]
        public static void ShowWindow()
        {
            GetWindow<BTTestRunner>("BT Test Runner");
        }

        private void OnEnable()
        {
            InitializeTestEnvironment();
        }

        private void InitializeTestEnvironment()
        {
            context = new BTContext();
            isRunning = false;
            lastUpdateTime = 0f;
            logMessages = "";
            
            isAlive = true;
            canAttack = true;
            health = 100f;
            attackCooldown = 0f;
            targetPosition = Vector3.zero;
            currentPosition = Vector3.zero;

            context.SetFloat("Health", health);
            context.SetBool("IsAlive", isAlive);
            context.SetBool("CanAttack", canAttack);
            context.SetVector3("TargetPosition", targetPosition);
            context.SetVector3("CurrentPosition", currentPosition);
            context.SetFloat("AttackCooldown", attackCooldown);
        }

        private void OnGUI()
        {
            GUILayout.Label("Behaviour Tree Test Runner", EditorStyles.boldLabel);
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            DrawStatusVariables();

            DrawControlButtons();

            DrawLogMessages();

            EditorGUILayout.EndScrollView();

            if (autoRun && isRunning)
            {
                if (Time.realtimeSinceStartup - lastUpdateTime >= autoRunInterval)
                {
                    UpdateBehaviorTree();
                    lastUpdateTime = Time.realtimeSinceStartup;
                    Repaint();
                }
            }
        }

        private void DrawStatusVariables()
        {
            GUILayout.Label("Status Variables", EditorStyles.boldLabel);
            
            EditorGUI.BeginChangeCheck();
            
            isAlive = EditorGUILayout.Toggle("Is Alive", isAlive);
            canAttack = EditorGUILayout.Toggle("Can Attack", canAttack);
            health = EditorGUILayout.Slider("Health", health, 0f, 100f);
            attackCooldown = EditorGUILayout.Slider("Attack Cooldown", attackCooldown, 0f, 5f);
            
            targetPosition = EditorGUILayout.Vector3Field("Target Position", targetPosition);
            currentPosition = EditorGUILayout.Vector3Field("Current Position", currentPosition);

            if (EditorGUI.EndChangeCheck())
            {
                UpdateContext();
            }
        }

        private void DrawControlButtons()
        {
            GUILayout.Label("Controls", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Build Test Tree"))
            {
                BuildTestTree();
            }

            if (!isRunning)
            {
                if (GUILayout.Button("Start Test"))
                {
                    StartTest();
                }
            }
            else
            {
                if (GUILayout.Button("Stop Test"))
                {
                    StopTest();
                }
            }

            autoRun = EditorGUILayout.Toggle("Auto Run", autoRun);
            if (autoRun)
            {
                autoRunInterval = EditorGUILayout.Slider("Update Interval", autoRunInterval, 0.01f, 1f);
            }

            if (GUILayout.Button("Step Once"))
            {
                StepOnce();
            }

            if (GUILayout.Button("Reset Test"))
            {
                ResetTest();
            }

            if (GUILayout.Button("Clear Log"))
            {
                logMessages = "";
            }
        }

        private void DrawLogMessages()
        {
            GUILayout.Label("Log Messages", EditorStyles.boldLabel);
            GUILayout.TextArea(logMessages, GUILayout.Height(200));
        }

        private void BuildTestTree()
        {
            try
            {
                behaviorTree = BTBuilder.Build()
                        .Selector("Root Selector")
                            .Sequence("NormalBoss")
                                .Condition("IsAlive", IsAlive)
                                .Do("CoolDown", CoolDown)
                                .Selector("AttackOrMove")
                                    .Sequence("Attack")
                                        .Do("Attack", Attack)
                                    .End()
                                    .Sequence("Move")
                                        .Do("MoveToTarget", MoveToTarget)
                                    .End()
                                .End()
                            .End()
                        .Do("Dead Motion", DeadMotion)
                        .End()
                        .Complete();

                LogMessage("Behaviour Tree built successfully!");
            }
            catch (System.Exception e)
            {
                LogMessage($"Error building tree: {e.Message}");
            }
        }

        private void StartTest()
        {
            if (behaviorTree == null)
            {
                BuildTestTree();
            }

            if (behaviorTree != null)
            {
                isRunning = true;
                lastUpdateTime = Time.realtimeSinceStartup;
                LogMessage("Test started!");
            }
        }

        private void StopTest()
        {
            isRunning = false;
            LogMessage("Test stopped!");
        }

        private void StepOnce()
        {
            if (behaviorTree == null)
            {
                BuildTestTree();
            }

            if (behaviorTree != null)
            {
                UpdateBehaviorTree();
            }
        }

        private void ResetTest()
        {
            StopTest();
            InitializeTestEnvironment();
            behaviorTree = null;
            LogMessage("Test reset!");
        }

        private void UpdateBehaviorTree()
        {
            if (behaviorTree == null || !isRunning) return;

            UpdateContext();
            
            var result = behaviorTree.Run(context);
            LogMessage($"Tree execution result: {result}");
        }

        private void UpdateContext()
        {
            context.SetFloat("Health", health);
            context.SetBool("IsAlive", isAlive);
            context.SetBool("CanAttack", canAttack);
            context.SetVector3("TargetPosition", targetPosition);
            context.SetVector3("CurrentPosition", currentPosition);
            context.SetFloat("AttackCooldown", attackCooldown);
        }

        private void LogMessage(string message)
        {
            string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
            logMessages += $"[{timestamp}] {message}\n";
        }

        private bool IsAlive(BTContext ctx)
        {
            bool alive = ctx.GetBool("IsAlive");
            LogMessage($"IsAlive check: {alive}");
            return alive;
        }

        private bool CanAttack(BTContext ctx)
        {
            bool canAttack = ctx.GetBool("CanAttack");
            float cooldown = ctx.GetFloat("AttackCooldown");
            bool result = canAttack && cooldown <= 0f;
            LogMessage($"CanAttack check: {result} (cooldown: {cooldown})");
            return result;
        }
        
        private NodeState CoolDown(BTContext ctx)
        {
            LogMessage("Cool Down");
            return NodeState.Success;
        }

        private NodeState Attack(BTContext ctx)
        {
            LogMessage("Executing Attack action");
            ctx.SetFloat("AttackCooldown", 2f);
            return NodeState.Success;
        }

        private NodeState MoveToTarget(BTContext ctx)
        {
            Vector3 target = ctx.GetVector3("TargetPosition");
            Vector3 current = ctx.GetVector3("CurrentPosition");
            
            float distance = Vector3.Distance(current, target);
            LogMessage($"MoveToTarget: Distance to target = {distance}");
            
            if (distance < 0.1f)
            {
                LogMessage("Reached target position");
                return NodeState.Success;
            }
            
            Vector3 direction = (target - current).normalized;
            ctx.SetVector3("CurrentPosition", current + direction * 0.1f);
            
            return NodeState.Running;
        }

        private NodeState DeadMotion(BTContext ctx)
        {
            LogMessage("Executing Dead Motion");
            return NodeState.Exit;
        }
    }
} 