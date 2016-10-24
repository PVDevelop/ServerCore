namespace PVDevelop.UCoach.Configuration
{
	public interface IConfigurationSectionProvider<out TSection>
		where TSection : class 
	{
		TSection GetSection();
	}
}
