现有bug ：
1.旋转状态 武器会碰到最后一个敌人才会停止，突破了设定的持续时间(已解决)
private void stopwhenspinning()
{
    rb.constraints = RigidbodyConstraints2D.FreezePosition;
    if(!wasstopped)
        spintimer = spinduration;
    wasstopped = true;
}//将wasstopped判定提前。在wasstoppped为真情况下进入，不对计时器重新赋值。
2.在投射模式 ，未进入状态时按下左键会出现辅助瞄准射线，同时辅助瞄准射线会从之前的位置开始
3.残影攻击状态，在里敌人较远时仍会朝向敌人的方向。
