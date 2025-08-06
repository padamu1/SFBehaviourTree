```

// 복잡한 AI 행동 트리 예시
var aiTree = BTBuilder.Build()
    .Selector("AI Root")
        .Sequence("Combat")
            .Condition("Has Enemy", context => context.GetValue<bool>("hasEnemy"))
            .Do("Attack", context => NodeState.Success)
        .End()
        .Sequence("Patrol")
            .Inverter("Not In Combat")
                .Condition("Has Enemy", context => context.GetValue<bool>("hasEnemy"))
            .End()
            .Do("Patrol", context => NodeState.Success)
            .Wait("Wait Between Patrol", 2.0f)
        .End()
        .Repeater("Idle Loop", 3)
            .Do("Idle Action", context => NodeState.Success)
        .End()
    .End()
    .Complete();

```
