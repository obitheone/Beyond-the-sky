using UnityEngine;

public class Credits : MonoBehaviour
{
	private float offset;
	public float speed = 29.0f;
	public GUIStyle style;
	public Rect viewArea;
	
	private void Start()
	{
		this.offset = this.viewArea.height;
	}
	
	private void Update()
	{
		this.offset -= Time.deltaTime * this.speed;
	}
	
	private void OnGUI()
	{
		GUI.BeginGroup(this.viewArea);
		
		var position = new Rect(0, this.offset, this.viewArea.width, this.viewArea.height);
		var text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.
 Quisque a mauris sit amet neque posuere molestie at laoreet lorem.
 Suspendisse accumsan pretium ante, sit amet tincidunt tortor tempor ac.
  
  
  
 Sed condimentum mi id nisi egestas non vulputate urna porttitor.
 Mauris sed mauris vitae velit imperdiet vulputate ut nec velit.
 Maecenas convallis posuere velit, quis interdum justo mattis vel.";
		
		GUI.Label(position, text, this.style);
		
		GUI.EndGroup();
	}
}