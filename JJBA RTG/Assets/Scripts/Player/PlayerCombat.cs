using UnityEngine.InputSystem.Interactions;

public sealed class PlayerCombat : Combat{

	PlayerInput input;

	internal override void m_Start(){
		ani.SetBool("Standless", !standOn);
		input = InputManager.input;
		Inputs();
	}

    public void Block() => Block(!stats.blocking);

    public void Pose() => Pose(!ani.GetBool("Posing"));

	public void Summon(){
		if (stats.stopped) return;
		
		standOn = !standOn;
		
		stand.gameObject.SetActive(standOn);

		ani.SetBool("Standless", !standOn);
	}

    internal override void Inputs()
	{
		input.Combat.Pose.started += _ =>   Pose();
		input.Combat.Summon.started += _ => Summon();
		input.Combat.Block.started += _ =>  Block();

		input.Combat.Atk.started += _ => 	Atk();
		input.Combat.SpAtk.started += _ =>  SpAtk();
		input.Combat.Strong.started += _ => Strong();
		input.Combat.Heavy.started += _ =>  Heavy();

		input.A.A1.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(1, 1);
			if (context.interaction is HoldInteraction) 	A(1, 2);
			if (context.interaction is MultiTapInteraction) A(1, 3);
		};
		input.A.A2.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(2, 1);
			if (context.interaction is HoldInteraction) 	A(2, 2);
			if (context.interaction is MultiTapInteraction) A(2, 3);
		};
		input.A.A3.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(3, 1);
			if (context.interaction is HoldInteraction) 	A(3, 2);
			if (context.interaction is MultiTapInteraction) A(3, 3);
		};

		input.A.A4.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(4, 1);
			if (context.interaction is HoldInteraction) 	A(4, 2);
			if (context.interaction is MultiTapInteraction) A(4, 3);
		};
		input.A.A5.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(5, 1);
			if (context.interaction is HoldInteraction) 	A(5, 2);
			if (context.interaction is MultiTapInteraction) A(5, 3);
		};
		input.A.A6.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(6, 1);
			if (context.interaction is HoldInteraction) 	A(6, 2);
			if (context.interaction is MultiTapInteraction) A(6, 3);
		};

		input.A.A7.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(7, 1);
			if (context.interaction is HoldInteraction) 	A(7, 2);
			if (context.interaction is MultiTapInteraction) A(7, 3);
		};
		input.A.A8.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(8, 1);
			if (context.interaction is HoldInteraction) 	A(8, 2);
			if (context.interaction is MultiTapInteraction) A(8, 3);
		};
		input.A.A9.started += context =>
		{
			if (context.interaction is TapInteraction) 		A(9, 1);
			if (context.interaction is HoldInteraction) 	A(9, 2);
			if (context.interaction is MultiTapInteraction) A(9, 3);
		};
	}
}